using System;
using Library.EventBus;

namespace Library.Shared.Events.Transaction
{
    public record DistributedTransactionResult
    {
        public Guid TransactionId { get; init; }
        public Guid EventId { get; init; }
        public EventType EventType { get; init; } = EventType.UNDEFINED;
        public DistributedTransactionState State { get; init; } = DistributedTransactionState.None;

        protected DistributedTransactionResult(Guid transactionId,
            Guid eventId,
            EventType eventType,
            DistributedTransactionState state = DistributedTransactionState.None)
        {
            TransactionId = transactionId;
            EventId = eventId;
            EventType = eventType;
            State = state;
        }

        public static DistributedTransactionResult Create(Guid transactionId,
            Guid eventId,
            EventType eventType,
            DistributedTransactionState state = DistributedTransactionState.None)
            => new DistributedTransactionResult(transactionId,
                eventId,
                eventType,
                state
            );

        public static DistributedTransactionResult CreateFrom(Event @event,
            DistributedTransactionState state = DistributedTransactionState.None)
            => new DistributedTransactionResult(@event.TransactionId,
                @event.EventId,
                @event.EventType,
                state
            );

        public static DistributedTransactionResult Default(Guid transactionId, Guid eventId)
            => new DistributedTransactionResult(transactionId, eventId, EventType.UNDEFINED);

        public static DistributedTransactionResult Interrupt(Guid transactionId, Guid eventId, EventType eventType)
            => new DistributedTransactionResult(transactionId, eventId, eventType, DistributedTransactionState.Interrupted);

        public bool IsCommitted => State == DistributedTransactionState.Committed;
        public bool IsRollbacked => State == DistributedTransactionState.Rollbacked;

        public bool IsCompleted => State == DistributedTransactionState.Committed
                                   || State == DistributedTransactionState.Rollbacked;

        public bool IsCancelled => State == DistributedTransactionState.None
                                   || State == DistributedTransactionState.Interrupted;

        public bool IsInCurrentTransaction(Guid receivedTransactionId) => TransactionId == receivedTransactionId;
    }
}