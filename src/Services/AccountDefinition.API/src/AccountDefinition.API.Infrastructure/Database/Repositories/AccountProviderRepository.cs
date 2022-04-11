using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountDefinition.API.Application.Database.Factories;
using AccountDefinition.API.Application.Database.Queries;
using AccountDefinition.API.Application.Database.Repositories;
using AccountDefinition.API.Domain.Entities;
using Dapper;
using Library.Database.Abstractions;
using Library.Shared.Resources;
using Npgsql;

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

        public async Task<AccountProvider> InsertAccountProviderAsync(AccountProvider accountProvider)
        {
            var query = await _resourceReader.ReadResourceAsync(
                QueryLocationFactory.PrepareQueryLocation(QueriesNames.InsertAccountProvider),
                QueryLocationFactory.QueriesAssembly
            );

            var parameters = AccountProviderParamsFactory.InsertAccountProviderParams(accountProvider.Provider,
                accountProvider.CreatedOn);

            await using (var connection = new NpgsqlConnection(_dbContext.ConnectionString))
            {
                return await connection.QuerySingleAsync<AccountProvider>(query, parameters);
            }
        }

        public async Task<long> DeleteAccountProviderByIdAsync(long accountProviderId)
        {
            var query = await _resourceReader.ReadResourceAsync(
                QueryLocationFactory.PrepareQueryLocation(QueriesNames.DeleteAccountProviderById),
                QueryLocationFactory.QueriesAssembly
            );

            var parameters = AccountProviderParamsFactory.DeleteAccountProviderByIdParams(accountProviderId);

            return await _dbContext.ExecuteAsync(query, parameters) > 0
                ? accountProviderId
                : default;
        }
    }
}