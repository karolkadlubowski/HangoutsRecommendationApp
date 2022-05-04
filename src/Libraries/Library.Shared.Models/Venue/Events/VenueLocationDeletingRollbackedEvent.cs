using Library.EventBus;

namespace Library.Shared.Models.Venue.Events
{
    public record VenueLocationDeletingRollbackedEvent : Event
    {
        public VenueLocationDeletingRollbackedEvent() => EventType = EventType.VENUE_LOCATION_DELETING_ROLLBACKED;
    }
}