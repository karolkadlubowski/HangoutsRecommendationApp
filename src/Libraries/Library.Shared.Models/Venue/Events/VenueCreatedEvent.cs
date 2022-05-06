using Library.EventBus;

namespace Library.Shared.Models.Venue.Events
{
    public record VenueCreatedEvent : Event
    {
        public VenueCreatedEvent() => EventType = EventType.VENUE_CREATED;
    }
}