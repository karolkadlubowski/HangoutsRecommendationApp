using System;

namespace Library.EventBus.Transaction
{
    public record DistributedTransactionResponse
    {
        public Guid TransactionId { get; init; }
        public Guid EventId { get; init; }
        public DistributedTransactionState State { get; init; } = DistributedTransactionState.None;

        public DistributedTransactionResponse(Guid transactionId,
            Guid eventId,
            DistributedTransactionState state = DistributedTransactionState.None)
        {
            TransactionId = transactionId;
            EventId = eventId;
            State = state;
        }

        public static DistributedTransactionResponse Default(Guid transactionId, Guid eventId)
            => new DistributedTransactionResponse(transactionId, eventId);
    }
}