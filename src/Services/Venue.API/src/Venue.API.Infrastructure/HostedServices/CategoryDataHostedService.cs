using System;
using System.Threading;
using System.Threading.Tasks;
using Library.Shared.Constants;
using Microsoft.Extensions.Hosting;
using NLog;
using Venue.API.Application.Abstractions;
using ILogger = Library.Shared.Logging.ILogger;

namespace Venue.API.Infrastructure.HostedServices
{
    public class CategoryDataHostedService : IHostedService
    {
        private readonly ICategoryDataService _categoryDataService;
        private readonly ILogger _logger;

        public CategoryDataHostedService(ICategoryDataService categoryDataService,
            ILogger logger)
        {
            _categoryDataService = categoryDataService;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                using (MappedDiagnosticsLogicalContext.SetScoped(LoggingConstants.Scope,
                           LoggingConstants.GetScopeValue($"{nameof(CategoryDataHostedService)}")))
                {
                    _logger.Info($"{nameof(CategoryDataHostedService)} hosted service started. Fetching categories data from the API");

                    var categories = await _categoryDataService.GetCategoriesAsync();
                    await _categoryDataService.StoreCategoriesInCacheAsync(categories);

                    _logger.Info($"{categories.Count} categories stored in the memory cache successfully");
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
            => await Task.Run(()
                => _logger.Info($"{nameof(CategoryDataHostedService)} hosted service stopped"));
    }
}