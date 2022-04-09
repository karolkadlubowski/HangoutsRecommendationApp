using Library.EventBus;

namespace Library.Shared.Models.AccountDefinition.Events
{
    public record AccountProviderDeletedEvent : EventWithoutData
    {
        public override string EventName => EventTypeConverter.Convert(EventType.ACCOUNT_PROVIDER_DELETED);
    }
}