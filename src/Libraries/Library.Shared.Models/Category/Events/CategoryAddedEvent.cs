using Library.EventBus;

namespace Library.Shared.Models.Category.Events
{
    public record CategoryAddedEvent : Event
    {
        public CategoryAddedEvent() => EventType = EventType.CATEGORY_ADDED;
    }
}