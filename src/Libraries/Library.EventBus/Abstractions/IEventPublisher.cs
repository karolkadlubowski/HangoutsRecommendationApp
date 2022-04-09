using System.Threading;
using System.Threading.Tasks;

namespace Library.EventBus.Abstractions
{
    public interface IEventPublisher
    {
        Task PublishAsync<TEvent>(string topic, TEvent @event, CancellationToken cancellationToken = default)
            where TEvent : Event<object>;
    }
}