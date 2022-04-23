using System.Threading;
using System.Threading.Tasks;
using Library.EventBus;

namespace UserProfile.API.Application.Abstractions
{
    public interface IEventHandlerStrategy
    {
        Task HandleEventAsync(Event @event, CancellationToken cancellationToken = default);
    }
}