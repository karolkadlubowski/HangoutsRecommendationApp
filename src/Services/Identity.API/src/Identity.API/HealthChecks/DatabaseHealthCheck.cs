using System;
using System.Threading;
using System.Threading.Tasks;
using Identity.API.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Identity.API.HealthChecks
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly IdentityDbContext _dbContext;

        private const string HealthCheckQuery = "SELECT 1;";

        public DatabaseHealthCheck(IdentityDbContext dbContext)
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
                return new HealthCheckResult(HealthStatus.Unhealthy, e.Message);
            }
        }
    } 
}