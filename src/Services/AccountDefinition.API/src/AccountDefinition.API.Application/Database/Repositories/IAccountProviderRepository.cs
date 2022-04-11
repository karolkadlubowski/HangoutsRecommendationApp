using System.Collections.Generic;
using System.Threading.Tasks;
using AccountDefinition.API.Domain.Entities;
using Library.Database.Abstractions;

namespace AccountDefinition.API.Application.Database.Repositories
{
    public interface IAccountProviderRepository : IDbRepository<AccountProvider>
    {
        Task<IReadOnlyList<AccountProvider>> GetAccountProvidersAsync();

        Task<AccountProvider> InsertAccountProviderAsync(AccountProvider accountProvider);
        Task<long> DeleteAccountProviderByIdAsync(long accountProviderId);
    }
}