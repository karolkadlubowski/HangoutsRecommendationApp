using System.Threading.Tasks;
using FileStorage.API.Application.Database.PersistenceModels;
using Library.Database.Abstractions;

namespace FileStorage.API.Application.Database.Repositories
{
    public interface IFolderInformationRepository : IDbRepository<FolderInformationPersistenceModel>
    {
        Task<FolderInformationPersistenceModel> GetFolderInformationByKeyAsync(string key);

        Task UpsertFolderInformationAsync(FolderInformationPersistenceModel persistenceModel);
    }
}