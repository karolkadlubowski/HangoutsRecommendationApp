using System.Threading.Tasks;
using Identity.API.Application.Database.PersistenceModels;
using Identity.API.Application.Database.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Infrastructure.Database.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IdentityDbContext _identityDbContext;

        public IdentityRepository(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<UserPersistenceModel> FindUserAsync(long userId)
            => await _identityDbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);


        public async Task<UserPersistenceModel> FindUserAsync(string email)
            => await _identityDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<bool> AnyUserWithEmailAsync(string email)
            => await _identityDbContext.Users.AnyAsync(u => u.Email == email);

        public async Task<bool> AddUserAsync(UserPersistenceModel user)
        {
            await _identityDbContext.Users.AddAsync(user);
            return await _identityDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUserAsync(UserPersistenceModel user)
        {
            _identityDbContext.Users.Update(user);
            return await _identityDbContext.SaveChangesAsync() > 0;
        }

    }
}