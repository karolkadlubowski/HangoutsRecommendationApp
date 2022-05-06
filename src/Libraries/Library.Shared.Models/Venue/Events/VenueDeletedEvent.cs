using Library.EventBus;

namespace Library.Shared.Models.Venue.Events
{
    public record VenueDeletedEvent : Event
    {
        public VenueDeletedEvent() => EventType = EventType.VENUE_DELETED;
    }
}