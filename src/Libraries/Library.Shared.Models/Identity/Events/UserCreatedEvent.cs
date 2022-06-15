using Library.EventBus;

namespace Library.Shared.Models.Identity.Events
{
    public record UserCreatedEvent: Event
    {
        public UserCreatedEvent() => EventType = EventType.USER_EMAIL_CHANGED;
    }
}