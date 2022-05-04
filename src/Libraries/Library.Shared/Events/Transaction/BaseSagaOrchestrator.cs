using System;
using System.Threading;
using System.Threading.Tasks;
using Library.EventBus;
using Library.Shared.Events.Abstractions;
using Library.Shared.Events.Transaction.Abstractions;
using Library.Shared.Logging;

namespace Library.Shared.Events.Transaction
{
    public abstract class BaseSagaOrchestrator : ISagaOrchestrator
    {
        protected readonly IEventAggregator _eventAggregator;
        protected readonly ILogger _logger;

        protected int OrchestratorLoopDelayInMilliseconds { get; init; } = 100;

        protected BaseSagaOrchestrator(IEventAggregator eventAggregator,
            ILogger logger)
        {
            _eventAggregator = eventAggregator;
            _logger = logger;
        }

        public virtual async Task<DistributedTransactionResult> OrchestrateTransactionAsync(Event firstEvent,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.Info($"> Distributed transaction #{firstEvent.TransactionId} began");

                await _eventAggregator.AggregateEventsAsync(cancellationToken);

                var distributedTransactionResult = new DistributedTransactionResult(firstEvent.TransactionId,
                    firstEvent.EventId,
                    firstEvent.EventType,
                    DistributedTransactionState.Began
                );

                _eventAggregator.TransactionUpdated += (_, currentTransactionResult) =>
                {
                    if (currentTransactionResult is not null &&
                        distributedTransactionResult.IsInCurrentTransaction(currentTransactionResult.TransactionId))
                    {
                        _logger.Info($"Distributed transaction #{firstEvent.TransactionId} updated with the new event #{currentTransactionResult.EventId}");

                        Task.Run(async () => distributedTransactionResult = await OrchestrateNextAsync(currentTransactionResult));

                        _logger.Info($"Current distributed transaction #{firstEvent.TransactionId} state is: '{distributedTransactionResult.State}'");
                    }
                };

                _logger.Trace($">> Waiting for distributed transaction #{firstEvent.TransactionId} updating events...");

                while (!cancellationToken.IsCancellationRequested)
                {
                    if (distributedTransactionResult.IsCompleted
                        || distributedTransactionResult.IsCancelled)
                        return distributedTransactionResult;

                    await Task.Delay(OrchestratorLoopDelayInMilliseconds);
                }

                _logger.Trace($"< Closing distributed transaction #{firstEvent.TransactionId} with the default result");
                return DistributedTransactionResult.Default(firstEvent.TransactionId, firstEvent.EventId);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return DistributedTransactionResult.Default(firstEvent.TransactionId, firstEvent.EventId);
            }
        }

        protected abstract Task<DistributedTransactionResult> OrchestrateNextAsync(DistributedTransactionResult currentTransactionResult);
    }
}