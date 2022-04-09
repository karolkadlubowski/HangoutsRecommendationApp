using System;
using System.Threading;
using System.Threading.Tasks;
using Library.Database.Abstractions;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AccountDefinition.API.HealthChecks
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly IDbContext _dbContext;

        private const string HealthCheckQuery = "SELECT 1;";

        public DatabaseHealthCheck(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                await _dbContext.ExecuteAsync(HealthCheckQuery);

                return new HealthCheckResult(HealthStatus.Healthy);
            }
            catch (Exception e)
            {
                return new HealthCheckResult(HealthStatus.Unhealthy, description: e.Message);
            }
        }
    }
}