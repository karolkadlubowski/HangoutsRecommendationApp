using System.Threading.Tasks;
using FileStorage.API.Application.Database.PersistenceModels;
using FileStorage.API.Application.Database.Repositories;
using MongoDB.Driver;

namespace FileStorage.API.Infrastructure.Database.Repositories
{
    public class BranchRepository : BaseDbRepository<BranchPersistenceModel>, IBranchRepository
    {
        public BranchRepository(FileStorageDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<BranchPersistenceModel> GetBranchAsync(string name)
            => await _collection
                .Find(b => b.Name == name)
                .FirstOrDefaultAsync();
    }
}