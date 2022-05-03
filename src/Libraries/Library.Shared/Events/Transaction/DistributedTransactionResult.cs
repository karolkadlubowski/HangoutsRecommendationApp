using System;

namespace Library.Shared.Events.Transaction
{
    public record DistributedTransactionResult
    {
        public Guid TransactionId { get; init; }
        public Guid EventId { get; init; }
        public DistributedTransactionState State { get; init; } = DistributedTransactionState.None;

        public DistributedTransactionResult(Guid transactionId,
            Guid eventId,
            DistributedTransactionState state = DistributedTransactionState.None)
        {
            TransactionId = transactionId;
            EventId = eventId;
            State = state;
        }

        public static DistributedTransactionResult Default(Guid transactionId, Guid eventId)
            => new DistributedTransactionResult(transactionId, eventId);

        public bool IsCommitted => State == DistributedTransactionState.Committed;
        public bool IsRollbacked => State == DistributedTransactionState.Rollbacked;

        public bool IsCompleted => State == DistributedTransactionState.Committed
                                   || State == DistributedTransactionState.Rollbacked;

        public bool IsInCurrentTransaction(Guid receivedTransactionId) => TransactionId == receivedTransactionId;
    }
}