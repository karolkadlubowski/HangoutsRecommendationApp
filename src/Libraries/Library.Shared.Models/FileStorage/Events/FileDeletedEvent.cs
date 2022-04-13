using Library.EventBus;

namespace Library.Shared.Models.FileStorage.Events
{
    public record FileDeletedEvent : Event
    {
        public FileDeletedEvent() => EventType = EventType.FILE_DELETED;
    }
}