using Library.EventBus;

namespace Library.Shared.Models.Venue.Events
{
    public record VenueLocationDeletedEvent : Event
    {
        public VenueLocationDeletedEvent() => EventType = EventType.VENUE_LOCATION_DELETED;
    }
}