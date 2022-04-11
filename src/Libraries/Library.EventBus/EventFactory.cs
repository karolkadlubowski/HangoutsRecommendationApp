using System;

namespace Library.EventBus
{
    public abstract class EventFactory<TEvent>
        where TEvent : Event, new()
    {
        public static TEvent CreateEvent<TData>(TData data)
            => new TEvent
            {
                Data = data
            };

        public static TEvent CreateEventWithoutData()
            => new TEvent();

        public static TEvent CreateEventInTransaction(Guid transactionId, object data)
            => new TEvent
            {
                TransactionId = transactionId,
                Data = data
            };
    }
}