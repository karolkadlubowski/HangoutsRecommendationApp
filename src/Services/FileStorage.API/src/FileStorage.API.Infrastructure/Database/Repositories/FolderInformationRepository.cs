using System.Threading.Tasks;
using FileStorage.API.Application.Database.PersistenceModels;
using FileStorage.API.Application.Database.Repositories;
using MongoDB.Driver;

namespace FileStorage.API.Infrastructure.Database.Repositories
{
    public class FolderInformationRepository : BaseDbRepository<FolderInformationPersistenceModel>, IFolderInformationRepository
    {
        public FolderInformationRepository(FileStorageDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<FolderInformationPersistenceModel> GetFolderInformationByKeyAsync(string key)
            => await _collection
                .Find(folder => folder.Key == key)
                .FirstOrDefaultAsync();

        public async Task UpsertFolderInformationAsync(FolderInformationPersistenceModel persistenceModel)
            => await _collection
                .ReplaceOneAsync(folder => folder.Key == persistenceModel.Key,
                    persistenceModel,
                    new ReplaceOptions { IsUpsert = true });
    }
}