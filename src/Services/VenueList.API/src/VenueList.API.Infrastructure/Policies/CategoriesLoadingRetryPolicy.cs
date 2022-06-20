using System;
using System.Threading.Tasks;
using Library.Shared.Logging;
using Library.Shared.Policies.Abstractions;
using Polly;
using VenueList.API.Application.Providers;

namespace VenueList.API.Infrastructure.Policies
{
    public class CategoriesLoadingRetryPolicy : IRetryPolicy
    {
        private readonly IAsyncPolicy _retryPolicy;

        private const int DefaultRetryAttemptDelayInSeconds = 30;

        public CategoriesLoadingRetryPolicy(IConfigurationProvider configurationProvider,
            ILogger logger)
        {
            var retryAttemptDelayInSeconds = GetRetryAttemptDelayInSeconds(configurationProvider);

            _retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryForeverAsync(retryAttempt
                    => TimeSpan.FromSeconds(retryAttemptDelayInSeconds), (exception, retryAttempt, _)
                    => logger.Warning(
                        $"{retryAttempt} attempt to load categories from the Category API due to an error: {exception.Message}\nNext attempt: {DateTime.Now.AddSeconds(retryAttemptDelayInSeconds)}"));
        }

        public async Task ExecutePolicyAsync(Func<Task> execute)
            => await _retryPolicy.ExecuteAsync(execute);

        private static int GetRetryAttemptDelayInSeconds(IConfigurationProvider configurationProvider)
        {
            var retryPolicyConfig = configurationProvider.GetConfiguration()
                .RetryPoliciesConfig?
                .GetPolicyConfig(nameof(CategoriesLoadingRetryPolicy));

            return retryPolicyConfig?.RetryAttemptDelayInSeconds ?? DefaultRetryAttemptDelayInSeconds;
        }
    }
}