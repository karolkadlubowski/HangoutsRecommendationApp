using System.Threading.Tasks;
using FileStorage.API.Application.Database.PersistenceModels;
using Library.Database.Abstractions;

namespace FileStorage.API.Application.Database.Repositories
{
    public interface IBranchRepository : IDbRepository<BranchPersistenceModel>
    {
        Task<BranchPersistenceModel> GetBranchAsync(string key);
    }
}