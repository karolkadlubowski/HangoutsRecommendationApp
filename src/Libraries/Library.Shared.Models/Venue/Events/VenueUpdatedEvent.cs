using Library.EventBus;

namespace Library.Shared.Models.Venue.Events
{
    public record VenueUpdatedEvent : Event
    {
        public VenueUpdatedEvent() => EventType = EventType.VENUE_UPDATED;
    }
}