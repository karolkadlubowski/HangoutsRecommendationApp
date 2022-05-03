using System.Collections.Generic;
using System.Linq;
using Library.Shared.Policies.Abstractions;

namespace Library.Shared.Policies
{
    public class RetryPolicyRegistry : IRetryPolicyRegistry
    {
        private readonly IEnumerable<IRetryPolicy> _retryPolicies;

        public RetryPolicyRegistry(IEnumerable<IRetryPolicy> retryPolicies)
            => _retryPolicies = retryPolicies;

        public IRetryPolicy GetPolicy<TPolicy>()
            where TPolicy : IRetryPolicy
            => _retryPolicies.SingleOrDefault(policy => policy.GetType() == typeof(TPolicy))
               ?? throw new KeyNotFoundException($"Retry policy type: '{typeof(TPolicy).Name}' not recognized");
    }
}