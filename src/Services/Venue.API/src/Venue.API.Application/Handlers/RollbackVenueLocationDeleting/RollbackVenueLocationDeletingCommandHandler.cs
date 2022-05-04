using System.Threading;
using System.Threading.Tasks;
using Library.Database.Transaction.Abstractions;
using Library.EventBus;
using Library.Shared.Events.Abstractions;
using Library.Shared.Events.Transaction;
using Library.Shared.Logging;
using MediatR;
using Venue.API.Application.Abstractions;

namespace Venue.API.Application.Handlers.RollbackVenueLocationDeleting
{
    public class RollbackVenueLocationDeletingCommandHandler : IRequestHandler<RollbackVenueLocationDeletingCommand, RollbackVenueLocationDeletingResponse>
    {
        private readonly IVenueLocationService _venueLocationService;
        private readonly IEventSender _eventSender;
        private readonly ITransactionManager _transactionManager;
        private readonly ILogger _logger;

        public RollbackVenueLocationDeletingCommandHandler(IVenueLocationService venueLocationService,
            IEventSender eventSender,
            ITransactionManager transactionManager,
            ILogger logger)
        {
            _venueLocationService = venueLocationService;
            _eventSender = eventSender;
            _transactionManager = transactionManager;
            _logger = logger;
        }

        public async Task<RollbackVenueLocationDeletingResponse> Handle(RollbackVenueLocationDeletingCommand request, CancellationToken cancellationToken)
        {
            using (var scope = _transactionManager.CreateScope())
            {
                _logger.Trace("> Database transaction began");

                var rollbackedVenueWithLocation = await _venueLocationService.RollbackLocationDeletingAsync(request);

                await _eventSender.SendEventAsync(EventBusTopics.Venue, rollbackedVenueWithLocation.FirstStoredEvent,
                    cancellationToken);

                scope.Complete();

                _logger.Trace("< Database transaction committed");

                return new RollbackVenueLocationDeletingResponse(rollbackedVenueWithLocation.FirstStoredEvent.TransactionId,
                    rollbackedVenueWithLocation.FirstStoredEvent.EventId,
                    rollbackedVenueWithLocation.FirstStoredEvent.EventType,
                    DistributedTransactionState.Rollbacked)
                {
                    VenueId = rollbackedVenueWithLocation.VenueId,
                    LocationId = rollbackedVenueWithLocation.LocationId.GetValueOrDefault()
                };
            }
        }
    }
}