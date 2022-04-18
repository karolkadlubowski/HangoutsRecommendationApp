using System;

namespace Library.EventBus
{
    public abstract class EventFactory<TEvent>
        where TEvent : Event, new()
    {
        public static TEvent CreateEvent<TData>(object entityId, TData data)
            => new TEvent
            {
                EntityId = entityId?.ToString(),
                Data = data
            };

        public static TEvent CreateEventWithoutData(object entityId)
            => new TEvent
            {
                EntityId = entityId?.ToString()
            };

        public static TEvent CreateEventInTransaction<TData>(Guid transactionId, object entityId, TData data)
            => new TEvent
            {
                TransactionId = transactionId,
                EntityId = entityId?.ToString(),
                Data = data
            };
    }
}