using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Library.Database;
using Npgsql;

namespace AccountDefinition.API.Infrastructure.Database
{
    public class AccountDefinitionDbContext : DbContext
    {
        public AccountDefinitionDbContext(string connectionString) : base(connectionString)
        {
        }

        public override async Task<IEnumerable<T>> QueryAsync<T>(string query, CancellationToken cancellationToken = default)
        {
            await using (var connection = new NpgsqlConnection(ConnectionString))
            {
                return await connection.QueryAsync<T>(query);
            }
        }

        public override async Task<IEnumerable<T>> QueryAsync<T>(string query, DynamicParameters parameters, CancellationToken cancellationToken = default)
        {
            await using (var connection = new NpgsqlConnection(ConnectionString))
            {
                return await connection.QueryAsync<T>(query, parameters);
            }
        }

        public override async Task<int> ExecuteAsync(string query, CancellationToken cancellationToken = default)
        {
            await using (var connection = new NpgsqlConnection(ConnectionString))
            {
                return await connection.ExecuteAsync(query);
            }
        }

        public override async Task<int> ExecuteAsync(string query, DynamicParameters parameters, CancellationToken cancellationToken = default)
        {
            await using (var connection = new NpgsqlConnection(ConnectionString))
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}