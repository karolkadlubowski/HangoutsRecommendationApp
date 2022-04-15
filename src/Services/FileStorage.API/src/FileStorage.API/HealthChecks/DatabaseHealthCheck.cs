using System;
using System.Threading;
using System.Threading.Tasks;
using FileStorage.API.Infrastructure.Database;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FileStorage.API.HealthChecks
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly FileStorageDbContext _dbContext;

        private const string HealthCheckCommand = "{ping:1}";

        public DatabaseHealthCheck(FileStorageDbContext dbContext)
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