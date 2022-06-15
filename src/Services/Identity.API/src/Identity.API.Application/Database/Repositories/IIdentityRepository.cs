using System.Threading.Tasks;
using Identity.API.Application.Database.PersistenceModels;

namespace Identity.API.Application.Database.Repositories
{
    public interface IIdentityRepository
    {
        Task<UserPersistenceModel> FindUserAsync(long userId);
        Task<UserPersistenceModel> FindUserAsync(string email);
        
        Task<bool> AnyUserWithEmailAsync(string email);
        
        Task<bool> AddUserAsync(UserPersistenceModel user);
        Task<bool> UpdateUserAsync(UserPersistenceModel user);
    }
}