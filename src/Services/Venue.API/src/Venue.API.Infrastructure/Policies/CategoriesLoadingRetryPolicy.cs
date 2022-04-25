using System;
using Library.Shared.Logging;
using Polly;
using Venue.API.Infrastructure.Policies.Abstractions;

namespace Venue.API.Infrastructure.Policies
{
    public class CategoriesLoadingRetryPolicy : IRetryPolicy
    {
        private const int RetryAttemptDelayInSeconds = 30;

        public IAsyncPolicy RetryPolicy { get; }

        public CategoriesLoadingRetryPolicy(ILogger logger)
            => RetryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryForeverAsync(retryAttempt
                    => TimeSpan.FromSeconds(RetryAttemptDelayInSeconds), (exception, retryAttempt, timeSpan)
                    => logger.Warning(
                        $"{retryAttempt} attempt to load categories from the Category API due to an error: {exception.Message}\nNext attempt: {DateTime.UtcNow.AddSeconds(RetryAttemptDelayInSeconds)}"));
    }
}