using System;
using System.Threading;
using System.Threading.Tasks;
using Library.Shared.Constants;
using Library.Shared.Policies.Abstractions;
using Microsoft.Extensions.Hosting;
using NLog;
using VenueList.API.Application.Abstractions;
using VenueList.API.Application.Providers;
using VenueList.API.Domain.Configuration;
using VenueList.API.Infrastructure.Policies;
using ILogger = Library.Shared.Logging.ILogger;

namespace VenueList.API.Infrastructure.HostedServices
{
    public class CategoryDataHostedService : BackgroundService
    {
        private readonly ICategoryDataService _categoryDataService;
        private readonly IRetryPolicy _retryPolicy;
        private readonly ILogger _logger;

        private readonly HostedServicesConfig _hostedServicesConfig;

        public CategoryDataHostedService(ICategoryDataService categoryDataService,
            IRetryPolicyRegistry retryPolicyRegistry,
            IConfigurationProvider configurationProvider,
            ILogger logger)
        {
            _categoryDataService = categoryDataService;
            _retryPolicy = retryPolicyRegistry.GetPolicy<CategoriesLoadingRetryPolicy>();
            _logger = logger;

            _hostedServicesConfig = configurationProvider.GetConfiguration().HostedServicesConfig;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            try
            {
                using (MappedDiagnosticsLogicalContext.SetScoped(LoggingConstants.Scope,
                           LoggingConstants.GetScopeValue($"{nameof(CategoryDataHostedService)}")))
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        _logger.Info($"{nameof(CategoryDataHostedService)} hosted service started. Fetching categories data from the API");

                        await _retryPolicy.ExecutePolicyAsync(async () =>
                        {
                            var categories = await _categoryDataService.GetCategoriesAsync();
                            await _categoryDataService.StoreCategoriesInCacheAsync(categories);

                            _logger.Info($"{categories.Count} categories stored in the memory cache successfully");
                        });

                        _logger.Info($"> Next hosted service iteration scheduled at: {DateTime.UtcNow.AddMinutes(_hostedServicesConfig.CategoryDataHostedServiceIntervalInMinutes)}");
                        await Task.Delay(TimeSpan.FromMinutes(_hostedServicesConfig.CategoryDataHostedServiceIntervalInMinutes));
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }
        }
    }
}