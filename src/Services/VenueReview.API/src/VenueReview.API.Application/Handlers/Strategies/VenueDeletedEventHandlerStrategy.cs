using System.Threading;
using System.Threading.Tasks;
using Library.EventBus;
using Library.Shared.Events;
using Library.Shared.Events.Transaction;
using Library.Shared.Models.Venue.Events.DataModels;
using MediatR;
using VenueReview.API.Application.Handlers.DeleteReviewsFromVenue;

namespace VenueReview.API.Application.Handlers.Strategies
{
    public class VenueDeletedEventHandlerStrategy : BaseEventHandlerStrategy
    {
        public VenueDeletedEventHandlerStrategy(IMediator mediator) : base(mediator)
        {
        }

        public override EventType EventType => EventType.VENUE_DELETED;

        public override async Task<DistributedTransactionResult> HandleEventAsync(Event @event, CancellationToken cancellationToken = default)
        {
            var dataModel = @event.GetData<VenueDeletedEventDataModel>();

            await _mediator.Send(new DeleteVenuesFromVenueCommand(dataModel.VenueId));

            return DistributedTransactionResult.Default(@event.TransactionId, @event.EventId);
        }
    }
}