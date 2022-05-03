using System;
using System.Threading.Tasks;
using Library.EventBus.Transaction;
using Library.Shared.Events.Abstractions;
using Library.Shared.Extensions;
using Library.Shared.Logging;

namespace Venue.API.Application.Handlers
{
    public class DeleteVenueOrchestrator
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILogger _logger;

        public DeleteVenueOrchestrator(IEventAggregator eventAggregator,
            ILogger logger)
        {
            _eventAggregator = eventAggregator;
            _logger = logger;
        }

        public async Task<DistributedTransactionResponse> OrchestrateTransactionAsync(Guid transactionId, Guid firstEventId)
        {
            await _eventAggregator.AggregateEventsAsync(default);

            var distributedTransactionResponse = new DistributedTransactionResponse(transactionId,
                firstEventId, DistributedTransactionState.Began);

            _eventAggregator.TransactionUpdated += (_, response) =>
            {
                _logger.Info($"RESPONSE: {response.ToJSON()}");
                if (response.TransactionId == distributedTransactionResponse.TransactionId)
                    distributedTransactionResponse = response;
            };

            while (distributedTransactionResponse.State != DistributedTransactionState.Committed)
            {
                _logger.Info("WAITING FOR TRANSACTION CHANGES...");
                await Task.Delay(1000);
            }

            return distributedTransactionResponse;
        }
    }
}