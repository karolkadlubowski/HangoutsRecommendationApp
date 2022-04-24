using System.Threading;
using System.Threading.Tasks;
using Library.Shared.Logging;
using Microsoft.Extensions.Hosting;
using Venue.API.Application.Abstractions;

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
            throw new System.NotImplementedException();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
            => await Task.Run(()
                => _logger.Info($"{nameof(CategoryDataHostedService)} hosted service stopped"));
    }
}