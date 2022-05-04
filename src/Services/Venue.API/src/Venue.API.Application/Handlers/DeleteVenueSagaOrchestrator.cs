using System.Threading.Tasks;
using Library.EventBus;
using Library.Shared.Events.Abstractions;
using Library.Shared.Events.Transaction;
using Library.Shared.Logging;

namespace Venue.API.Application.Handlers
{
    public class DeleteVenueSagaOrchestrator : BaseSagaOrchestrator
    {
        public DeleteVenueSagaOrchestrator(IEventAggregator eventAggregator, ILogger logger)
            : base(eventAggregator, logger)
        {
        }

        protected override Task<DistributedTransactionResult> OrchestrateNextAsync(DistributedTransactionResult currentTransactionResult)
            => Task.FromResult(currentTransactionResult.EventType switch
            {
                EventType.VENUE_LOCATION_DELETING_ROLLBACKED => DistributedTransactionResult.Create(currentTransactionResult.TransactionId,
                    currentTransactionResult.EventId,
                    EventType.VENUE_LOCATION_DELETING_ROLLBACKED,
                    DistributedTransactionState.Rollbacked),
                _ => DistributedTransactionResult.Interrupt(currentTransactionResult.TransactionId,
                    currentTransactionResult.EventId,
                    currentTransactionResult.EventType)
            });
    }
}