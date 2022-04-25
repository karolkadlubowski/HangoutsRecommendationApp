namespace Venue.API.Infrastructure.Policies.Abstractions
{
    public interface IRetryPolicyRegistry
    {
        IRetryPolicy GetPolicy<TPolicy>() where TPolicy : IRetryPolicy;
    }
}