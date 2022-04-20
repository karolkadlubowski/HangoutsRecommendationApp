using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Venue.API.Infrastructure.Database;

namespace Venue.API.HealthChecks
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly VenueDbContext _dbContext;

        private const string HealthCheckQuery = "SELECT 1;";

        public DatabaseHealthCheck(VenueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync(HealthCheckQuery);

                return new HealthCheckResult(HealthStatus.Healthy);
            }
            catch (Exception e)
            {
                return new HealthCheckResult(HealthStatus.Unhealthy, description: e.Message);
            }
        }
    }
}