using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountDefinition.API.Application.Database.Queries;
using AccountDefinition.API.Application.Database.Repositories;
using AccountDefinition.API.Domain.Entities;
using Library.Database.Abstractions;
using Library.Shared.Resources;

namespace AccountDefinition.API.Infrastructure.Database.Repositories
{
    public class AccountProviderRepository : IAccountProviderRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IResourceReader _resourceReader;

        public AccountProviderRepository(IDbContext dbContext, IResourceReader resourceReader)
        {
            _dbContext = dbContext;
            _resourceReader = resourceReader;
        }
        
        public async Task<IReadOnlyList<AccountProvider>> GetAccountProvidersAsync()
        {
            var query = await _resourceReader.ReadResourceAsync(
                QueryLocationFactory.PrepareQueryLocation(QueriesNames.SelectAccountProviders),
                QueryLocationFactory.QueriesAssembly
            );

            return (await _dbContext.QueryAsync<AccountProvider>(query))
                .ToList();
        }
    }
}