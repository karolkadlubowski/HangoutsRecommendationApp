using System;

namespace Library.EventBus
{
    public abstract class EventFactory<TEvent> where TEvent : Event, new()
    {
        public static TEvent CreateEventInTransaction(Guid transactionId, object data)
            => new TEvent
            {
                TransactionId = transactionId,
                Data = data
            };
    }
}