using Library.EventBus;

namespace Library.Shared.Models.Venue.Events
{
    public record VenueCreatedWithoutLocationEvent : Event
    {
        public VenueCreatedWithoutLocationEvent() => EventType = EventType.VENUE_CREATED_WITHOUT_LOCATION;
    }
}