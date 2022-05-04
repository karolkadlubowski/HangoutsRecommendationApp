using Library.EventBus;

namespace Library.Shared.Models.Location.Events
{
    public record LocationDeletingFailedEvent : Event
    {
        public LocationDeletingFailedEvent() => EventType = EventType.LOCATION_DELETING_FAILED;
    }
}