using System.Threading.Tasks;
using FileStorage.API.Application.Database.PersistenceModels;
using Library.Database.Abstractions;

namespace FileStorage.API.Application.Database.Repositories
{
    public interface IFolderRepository : IDbRepository<FolderPersistenceModel>
    {
        Task<FolderPersistenceModel> GetFolderByKeyAsync(string key);

        Task UpsertFolderAsync(FolderPersistenceModel persistenceModel);
    }
}