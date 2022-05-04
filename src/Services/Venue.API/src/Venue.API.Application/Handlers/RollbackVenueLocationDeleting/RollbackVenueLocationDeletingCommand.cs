using Library.EventBus;
using Library.Shared.Events.Transaction;
using MediatR;

namespace Venue.API.Application.Handlers.RollbackVenueLocationDeleting
{
    public record RollbackVenueLocationDeletingCommand : DistributedTransactionEventRequest, IRequest<RollbackVenueLocationDeletingResponse>
    {
        public RollbackVenueLocationDeletingCommand(Event @event, long venueId, long locationId) :
            base(@event)
        {
            VenueId = venueId;
            LocationId = locationId;
        }

        public long VenueId { get; init; }
        public long LocationId { get; init; }
    }
}