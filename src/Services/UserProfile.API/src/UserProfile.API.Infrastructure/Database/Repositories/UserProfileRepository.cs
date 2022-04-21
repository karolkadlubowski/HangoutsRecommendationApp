using Library.Shared.AppConfigs;
using Library.Shared.Caching;
using Microsoft.Extensions.Caching.Distributed;
using UserProfile.API.Application.Database.PersistenceModels;
using UserProfile.API.Application.Database.Repositories;

namespace UserProfile.API.Infrastructure.Database.Repositories
{
    public class UserProfileRepository : DistributedCacheRepository<UserProfilePersistenceModel>, IUserProfileRepository
    {
        public UserProfileRepository(IDistributedCache cache, CacheConfig cacheConfig)
            : base(cache, cacheConfig)
        {
        }
    }
}