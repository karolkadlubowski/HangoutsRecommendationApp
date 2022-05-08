using System.Threading;
using System.Threading.Tasks;
using Library.EventBus;
using Library.Shared.Events.Transaction;

namespace Library.Shared.Events.Abstractions
{
    public interface IEventHandlerStrategy
    {
        EventType EventType { get; }

        Task<DistributedTransactionResult> HandleEventAsync(Event @event, CancellationToken cancellationToken = default);
    }
}