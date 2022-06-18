using Library.Shared.AppConfigs;

namespace VenueList.API.Domain.Configuration
{
    public record VenueRetryPoliciesConfig : RetryPoliciesConfig<RetryPolicyConfig>;
}