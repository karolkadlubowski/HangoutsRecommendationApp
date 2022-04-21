using Library.EventBus;

namespace Library.Shared.Models.Category.Events
{
    public record CategoryDeletedEvent : Event
    {
        public CategoryDeletedEvent() => EventType = EventType.CATEGORY_DELETED;
    }
}