using Library.EventBus;

namespace Library.Shared.Models.FileStorage.Events
{
    public record FileAddedEvent : Event
    {
        public FileAddedEvent() => EventType = EventType.FILE_ADDED;
    }
}