namespace Library.Shared.Policies.Abstractions
{
    public interface IRetryPolicyRegistry
    {
        IRetryPolicy GetPolicy<TPolicy>() where TPolicy : IRetryPolicy;
    }
}