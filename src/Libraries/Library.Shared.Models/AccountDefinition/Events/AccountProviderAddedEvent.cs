using Library.EventBus;

namespace Library.Shared.Models.AccountDefinition.Events
{
    public record AccountProviderAddedEvent : EventWithoutData
    {
        public override string EventName => EventTypeConverter.Convert(EventType.ACCOUNT_PROVIDER_ADDED);
    }
}