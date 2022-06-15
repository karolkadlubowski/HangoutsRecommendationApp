using Library.EventBus;

namespace Library.Shared.Models.VenueReview.Events
{
    public record VenueReviewDeletedEvent : Event
    {
        public VenueReviewDeletedEvent() => EventType = EventType.VENUE_REVIEW_DELETED;
    }
}