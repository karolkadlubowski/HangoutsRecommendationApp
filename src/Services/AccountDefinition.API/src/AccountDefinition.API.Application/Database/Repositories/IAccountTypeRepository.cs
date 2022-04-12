using System.Collections.Generic;
using System.Threading.Tasks;
using AccountDefinition.API.Application.Database.PersistenceModels;
using Library.Database.Abstractions;

namespace AccountDefinition.API.Application.Database.Repositories
{
    public interface IAccountTypeRepository : IDbRepository<AccountTypePersistenceModel>
    {
        Task<IReadOnlyList<AccountTypePersistenceModel>> GetAccountTypesAsync();
    }
}