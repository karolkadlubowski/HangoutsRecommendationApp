using System.Threading;
using System.Threading.Tasks;
using Library.EventBus;

namespace Library.Shared.Events.Abstractions
{
    public interface IEventSender
    {
        Task<Event> SendEventAsync(string topic, Event @event,
            CancellationToken cancellationToken = default);
    }
}