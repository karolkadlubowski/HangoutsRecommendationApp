using Library.EventBus;

namespace Library.Shared.Models.AccountDefinition.Events
{
    public record AccountProviderDeletedEvent : Event
    {
        public AccountProviderDeletedEvent() => EventType = EventType.ACCOUNT_PROVIDER_DELETED;
    }
}