using System.Collections.Generic;

namespace Library.Shared.AppConfigs
{
    public abstract record RetryPoliciesConfig<TPolicyConfig> where TPolicyConfig : RetryPolicyConfig
    {
        public IDictionary<string, TPolicyConfig> Policies { get; init; } = new Dictionary<string, TPolicyConfig>();

        public TPolicyConfig GetPolicyConfig(string policyName)
            => Policies.TryGetValue(policyName, out var policyConfig)
                ? policyConfig
                : null;
    }
}