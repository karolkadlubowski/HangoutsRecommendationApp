using System.Threading;
using System.Threading.Tasks;
using Library.Database.Transaction.Abstractions;
using Library.EventBus;
using Library.Shared.Events.Abstractions;
using Library.Shared.Logging;
using MediatR;
using Venue.API.Application.Abstractions;

namespace Venue.API.Application.Features.DeleteVenue
{
    public class DeleteVenueCommandHandler : IRequestHandler<DeleteVenueCommand, DeleteVenueResponse>
    {
        private readonly IVenueService _venueService;
        private readonly ITransactionManager _transactionManager;
        private readonly IEventSender _eventSender;
        private readonly ILogger _logger;

        public DeleteVenueCommandHandler(IVenueService venueService,
            ITransactionManager transactionManager,
            IEventSender eventSender,
            ILogger logger)
        {
            _venueService = venueService;
            _transactionManager = transactionManager;
            _eventSender = eventSender;
            _logger = logger;
        }

        public async Task<DeleteVenueResponse> Handle(DeleteVenueCommand request, CancellationToken cancellationToken)
        {
            using (var scope = _transactionManager.CreateScope())
            {
                _logger.Trace("> Database transaction began");

                var venue = await _venueService.DeleteVenueAsync(request);

                await _eventSender.SendEventAsync(EventBusTopics.Venue, venue.FirstStoredEvent,
                    cancellationToken);

                scope.Complete();

                _logger.Trace("< Database transaction committed");

                return new DeleteVenueResponse { DeletedVenueId = venue.VenueId, DeletedLocationId = venue.LocationId };
            }
        }
    }
}