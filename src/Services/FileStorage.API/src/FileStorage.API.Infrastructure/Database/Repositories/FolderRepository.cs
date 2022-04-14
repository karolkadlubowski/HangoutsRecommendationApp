using System.Threading.Tasks;
using FileStorage.API.Application.Database.PersistenceModels;
using FileStorage.API.Application.Database.Repositories;
using MongoDB.Driver;

namespace FileStorage.API.Infrastructure.Database.Repositories
{
    public class FolderRepository : BaseDbRepository<FolderPersistenceModel>, IFolderRepository
    {
        public FolderRepository(FileStorageDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<FolderPersistenceModel> GetFolderByKeyAsync(string key)
            => await _collection
                .Find(folder => folder.Key == key)
                .FirstOrDefaultAsync();

        public async Task<bool> UpdateFolderAsync(FolderPersistenceModel persistenceModel)
            => (await _collection
                    .ReplaceOneAsync(folder => folder.Key == persistenceModel.Key,
                        persistenceModel))
                .IsAcknowledged;

        public async Task<bool> UpsertFolderAsync(FolderPersistenceModel persistenceModel)
            => (await _collection
                    .ReplaceOneAsync(folder => folder.Key == persistenceModel.Key,
                        persistenceModel,
                        new ReplaceOptions { IsUpsert = true }))
                .IsAcknowledged;

        public async Task<bool> DeleteFolderAsync(string key)
            => (await _collection
                    .DeleteOneAsync(folder => folder.Key == key))
                .DeletedCount > 0;
    }
}