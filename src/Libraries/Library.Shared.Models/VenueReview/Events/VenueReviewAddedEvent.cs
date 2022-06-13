using Library.EventBus;

namespace Library.Shared.Models.VenueReview.Events
{
    public record VenueReviewAddedEvent : Event
    {
        public VenueReviewAddedEvent() => EventType = EventType.VENUE_REVIEW_ADDED;
    }
}