using System;
using System.Threading;
using System.Threading.Tasks;
using Library.EventBus.Transaction;

namespace Library.Shared.Events.Abstractions
{
    public interface IEventAggregator
    {
        event EventHandler<DistributedTransactionResponse> TransactionUpdated;

        Task AggregateEventsAsync(CancellationToken cancellationToken = default);
    }
}