using System.Threading;
using System.Threading.Tasks;
using Library.EventBus;
using Library.EventBus.Transaction;

namespace Library.Shared.Events.Abstractions
{
    public interface IEventHandlerStrategy
    {
        EventType EventType { get; }

        Task<DistributedTransactionResponse> HandleEventAsync(Event @event, CancellationToken cancellationToken = default);
    }
}