using Library.EventBus;

namespace Library.Shared.Models.AccountDefinition.Events
{
    public record AccountProviderAddedEvent : Event
    {
        public AccountProviderAddedEvent() => EventType = EventType.ACCOUNT_PROVIDER_ADDED;
    }
}