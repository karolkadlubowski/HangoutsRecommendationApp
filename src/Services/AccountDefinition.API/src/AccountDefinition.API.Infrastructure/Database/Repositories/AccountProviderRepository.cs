using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountDefinition.API.Application.Database.Queries;
using AccountDefinition.API.Application.Database.Repositories;
using AccountDefinition.API.Domain.Entities;
using AccountDefinition.API.Domain.ValueObjects;
using Dapper;
using Library.Database;
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

        public async Task<AccountProvider> InsertAccountProviderAsync(string provider)
        {
            var query = await _resourceReader.ReadResourceAsync(
                QueryLocationFactory.PrepareQueryLocation(QueriesNames.InsertAccountProvider),
                QueryLocationFactory.QueriesAssembly
            );

            var parameters = new DynamicParametersBuilder()
                .Append("@Provider", new ProviderName(provider).Value)
                .Build();

            await using (var connection = new NpgsqlConnection(_dbContext.ConnectionString))
            {
                return await connection.QuerySingleAsync<AccountProvider>(query, parameters);
            }
        }
    }
}