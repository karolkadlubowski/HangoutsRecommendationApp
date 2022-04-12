using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountDefinition.API.Application.Database.PersistenceModels;
using AccountDefinition.API.Application.Database.Queries;
using AccountDefinition.API.Application.Database.Repositories;
using Library.Database.Abstractions;
using Library.Shared.Resources;

namespace AccountDefinition.API.Infrastructure.Database.Repositories
{
    public class AccountTypeRepository : IAccountTypeRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IResourceReader _resourceReader;

        public AccountTypeRepository(IDbContext dbContext, IResourceReader resourceReader)
        {
            _dbContext = dbContext;
            _resourceReader = resourceReader;
        }

        public async Task<IReadOnlyList<AccountTypePersistenceModel>> GetAccountTypesAsync()
        {
            var query = await _resourceReader.ReadResourceAsync(
                QueryLocationFactory.PrepareQueryLocation(QueriesNames.SelectAccountTypes),
                QueryLocationFactory.QueriesAssembly
            );

            return (await _dbContext.QueryAsync<AccountTypePersistenceModel>(query))
                .ToList();
        }
    }
}