using Library.Shared.AppConfigs;

namespace Venue.API.Domain.Configuration
{
    public record VenueRetryPoliciesConfig : RetryPoliciesConfig<RetryPolicyConfig>;
}