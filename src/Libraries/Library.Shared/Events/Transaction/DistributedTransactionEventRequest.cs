using Library.EventBus;

namespace Library.Shared.Events.Transaction
{
    public record DistributedTransactionEventRequest
    {
        public Event Event { get; init; }

        public DistributedTransactionEventRequest(Event @event) => Event = @event;
    }
}