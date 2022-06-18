using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using VenueList.API.Infrastructure.Database;

namespace VenueList.API.HealthChecks
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly VenueListDbContext _dbContext;

        private const string HealthCheckCommand = "{ping:1}";

        public DatabaseHealthCheck(VenueListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
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