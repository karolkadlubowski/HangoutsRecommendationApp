using System.Threading.Tasks;
using Identity.API.Application.Database.PersistenceModels;

namespace Identity.API.Application.Database.Repositories
{
    public interface IIdentityRepository
    {
        Task<bool> AddUserAsync(UserPersistenceModel user);
    }
}