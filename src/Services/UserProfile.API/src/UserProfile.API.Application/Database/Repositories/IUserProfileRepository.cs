using System.Threading.Tasks;
using Library.Shared.Caching.Abstractions;
using UserProfile.API.Application.Database.PersistenceModels;

namespace UserProfile.API.Application.Database.Repositories
{
    public interface IUserProfileRepository : IDistributedCacheRepository<UserProfilePersistenceModel>
    {
        Task<UserProfilePersistenceModel> GetUserProfileAsync(long userId);
        Task UpdateUserProfileAsync(long userId, UserProfilePersistenceModel userProfile);
    }
}