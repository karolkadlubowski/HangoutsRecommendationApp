using System.Collections.Generic;
using System.Threading.Tasks;
using AccountDefinition.API.Application.Database.PersistenceModels;
using AccountDefinition.API.Domain.Entities;
using Library.Database.Abstractions;

namespace AccountDefinition.API.Application.Database.Repositories
{
    public interface IAccountProviderRepository : IDbRepository<AccountProviderPersistenceModel>
    {
        Task<IReadOnlyList<AccountProviderPersistenceModel>> GetAccountProvidersAsync();

        Task<AccountProviderPersistenceModel> InsertAccountProviderAsync(AccountProvider accountProvider);
        Task<long> DeleteAccountProviderByIdAsync(long accountProviderId);
    }
}