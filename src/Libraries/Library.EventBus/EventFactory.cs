using System;

namespace Library.EventBus
{
    public class EventFactory<TEvent> where TEvent : Event<object>, new()
    {
        public static TEvent CreateEventInTransaction(Guid transactionId, object data)
            => new TEvent
            {
                TransactionId = transactionId,
                Data = data
            };
    }
}