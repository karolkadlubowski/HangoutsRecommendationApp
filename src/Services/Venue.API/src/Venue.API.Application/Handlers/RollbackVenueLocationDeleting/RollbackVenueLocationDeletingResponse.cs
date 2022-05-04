using System;
using Library.EventBus;
using Library.Shared.Events.Transaction;

namespace Venue.API.Application.Handlers.RollbackVenueLocationDeleting
{
    public record RollbackVenueLocationDeletingResponse : DistributedTransactionResult
    {
        public long VenueId { get; init; }
        public long LocationId { get; init; }

        public RollbackVenueLocationDeletingResponse(Guid transactionId,
            Guid eventId,
            EventType eventType,
            DistributedTransactionState state = DistributedTransactionState.None)
            : base(transactionId, eventId, eventType, state)
        {
        }
    }
}