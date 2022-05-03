using System;
using System.Threading;
using System.Threading.Tasks;
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

        public virtual async Task<DistributedTransactionResult> OrchestrateTransactionAsync(Guid transactionId, Guid firstEventId,
            CancellationToken cancellationToken = default)
        {
            _logger.Info($"> Distributed transaction #{transactionId} began");

            await _eventAggregator.AggregateEventsAsync(cancellationToken);

            var distributedTransactionResult = new DistributedTransactionResult(transactionId,
                firstEventId,
                DistributedTransactionState.Began
            );

            _eventAggregator.TransactionUpdated += (_, currentTransactionResult) =>
            {
                if (currentTransactionResult is not null &&
                    distributedTransactionResult.IsInCurrentTransaction(currentTransactionResult.TransactionId))
                {
                    _logger.Info($"Distributed transaction #{transactionId} updated with the new event #{currentTransactionResult.EventId}");

                    Task.Run(async () => await OrchestrateNextAsync(currentTransactionResult));

                    _logger.Info($"Current distributed transaction #{transactionId} state is: '{distributedTransactionResult.State}'");
                }
            };

            _logger.Trace($">> Waiting for distributed transaction #{transactionId} updating events...");
            while (!cancellationToken.IsCancellationRequested)
            {
                if (distributedTransactionResult.IsCompleted)
                    return distributedTransactionResult;

                await Task.Delay(OrchestratorLoopDelayInMilliseconds);
            }

            _logger.Trace($"< Closing distributed transaction #{transactionId} with the default result");
            return DistributedTransactionResult.Default(transactionId, firstEventId);
        }

        protected abstract Task<DistributedTransactionResult> OrchestrateNextAsync(DistributedTransactionResult currentTransactionResult);
    }
}