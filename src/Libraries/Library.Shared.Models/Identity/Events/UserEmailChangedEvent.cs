using Library.EventBus;

namespace Library.Shared.Models.Identity.Events
{
    public record UserEmailChangedEvent : Event
    {
        public UserEmailChangedEvent() => EventType = EventType.USER_CREATED;
    }
}