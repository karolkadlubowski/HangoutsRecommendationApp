using System.Threading;
using System.Threading.Tasks;

namespace UserProfile.API.Application.Abstractions
{
    public interface IEventAggregator
    {
        Task AggregateEventsAsync(CancellationToken cancellationToken = default);
    }
}