using System;

namespace Library.EventBus
{
    public record Event
    {
        public Guid EventId { get; init; } = Guid.NewGuid();
        public DateTime CreatedOn { get; init; } = DateTime.UtcNow;
        public Guid TransactionId { get; init; } = Guid.NewGuid();
        public EventType EventType { get; init; }

        public object Data { get; init; }

        public string EventName => EventTypeConverter.Convert(EventType);

        public virtual TData GetData<TData>() where TData : class
            => Data as TData;
    }
}