using System;
using System.Threading;
using System.Threading.Tasks;
using Library.Shared.Events.Transaction;

namespace Library.Shared.Events.Abstractions
{
    public interface IEventAggregator
    {
        event EventHandler<DistributedTransactionResult> TransactionUpdated;

        Task AggregateEventsAsync(CancellationToken cancellationToken = default);
    }
}