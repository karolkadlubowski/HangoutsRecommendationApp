using System.Reflection;
using Library.Shared.Extensions;
using Library.Shared.Policies;
using Library.Shared.Policies.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Shared.DI.Configs
{
    public static class RetryPoliciesDIConfig
    {
        public static IServiceCollection AddRetryPolicyRegistry(this IServiceCollection services)
            => services
                .AddSingleton<IRetryPolicyRegistry, RetryPolicyRegistry>();
    }
}