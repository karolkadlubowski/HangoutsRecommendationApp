using System.Threading;
using System.Threading.Tasks;
using Library.EventBus;

namespace Library.Shared.Events.Abstractions
{
    public interface IEventHandlerStrategy
    {
        Task HandleEventAsync(Event @event, CancellationToken cancellationToken = default);
    }
}