using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using UserProfile.API.Application.Database.PersistenceModels;
using UserProfile.API.Application.Database.Repositories;

namespace UserProfile.API.HealthChecks
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly IUserProfileRepository _userProfileRepository;

        private const string CacheKey = nameof(DatabaseHealthCheck);

        public DatabaseHealthCheck(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                await _userProfileRepository.SetValueAsync(CacheKey, new UserProfilePersistenceModel());
                await _userProfileRepository.DeleteValueAsync(CacheKey);

                return new HealthCheckResult(HealthStatus.Healthy);
            }
            catch (Exception e)
            {
                return new HealthCheckResult(HealthStatus.Unhealthy, e.Message);
            }
        }
    }
}