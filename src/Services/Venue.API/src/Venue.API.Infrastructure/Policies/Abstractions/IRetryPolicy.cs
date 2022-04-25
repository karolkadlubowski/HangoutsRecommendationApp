using Polly;

namespace Venue.API.Infrastructure.Policies.Abstractions
{
    public interface IRetryPolicy
    {
        IAsyncPolicy RetryPolicy { get; }
    }
}