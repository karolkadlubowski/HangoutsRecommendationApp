using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using VenueReview.API.Infrastructure.Database;

namespace Category.API.HealthChecks
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly VenueReviewDbContext _dbContext;

        private const string HealthCheckCommand = "{ping:1}";

        public DatabaseHealthCheck(VenueReviewDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                await _dbContext.ExecuteAsync(HealthCheckCommand);

                return new HealthCheckResult(HealthStatus.Healthy);
            }
            catch (Exception e)
            {
                return new HealthCheckResult(HealthStatus.Unhealthy, description: e.Message);
            }
        }
    }
}