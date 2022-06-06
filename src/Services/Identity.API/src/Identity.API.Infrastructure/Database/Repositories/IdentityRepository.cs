using System.Threading.Tasks;
using Identity.API.Application.Database.PersistenceModels;
using Identity.API.Application.Database.Repositories;

namespace Identity.API.Infrastructure.Database.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IdentityDbContext _identityDbContext;

        public IdentityRepository(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<bool> AddUserAsync(UserPersistenceModel user)
        {
            await _identityDbContext.Users.AddAsync(user);
            return await _identityDbContext.SaveChangesAsync() > 0;
        }
    }
}