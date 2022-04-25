using System.Reflection;
using Library.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Venue.API.Infrastructure.Policies;
using Venue.API.Infrastructure.Policies.Abstractions;
using ILogger = NLog.ILogger;

namespace Venue.API.DI
{
    public static class PolicyRegistryDIConfig
    {
        public static IServiceCollection AddPolicyRegistry(this IServiceCollection services, ILogger logger)
        {
            // var policyRegistry = new PolicyRegistry
            // {
            //     {
            //         RetryPoliciesNames.CategoriesLoading, RetryPoliciesAbstractFactory.CreateRetryPolicy(
            //             RetryPoliciesNames.CategoriesLoading, new RetryPolicyConfig(), logger)
            //     }
            // };

            return services
                // .AddSingleton<IReadOnlyPolicyRegistry<string>>(policyRegistry)
                .AddSingleton<IRetryPolicyRegistry, RetryPolicyRegistry>()
                .RegisterAllTypes<IRetryPolicy>(new[] { Assembly.Load("Venue.API.Infrastructure") }, ServiceLifetime.Singleton);
        }
    }
}