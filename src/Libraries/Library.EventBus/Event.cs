using System;
using System.Text.Json;

namespace Library.EventBus
{
    public record Event
    {
        public Guid EventId { get; init; } = Guid.NewGuid();
        public EventType EventType { get; init; }
        public Guid TransactionId { get; init; } = Guid.NewGuid();
        public string EntityId { get; init; }
        public DateTime CreatedOn { get; init; } = DateTime.UtcNow;

        public string Data { get; init; }

        public string EventName => EventTypeConverter.Convert(EventType);

        public virtual TData GetData<TData>() where TData : class
            => JsonSerializer.Deserialize<TData>(Data)
               ?? throw new ArgumentNullException(nameof(Data));
    }
}