using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Library.EventBus;

namespace Library.Shared.Events.Abstractions
{
    public interface IEventAggregator
    {
        ConcurrentDictionary<Guid, HashSet<Event>> EventsTransactions { get; }

        Task AggregateEventsAsync(CancellationToken cancellationToken = default);
    }
}