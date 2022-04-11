using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Library.Database.Abstractions;

namespace Library.Database
{
    public abstract class DbContext : IDbContext
    {
        protected DbContext(string connectionString) => ConnectionString = connectionString;

        public string ConnectionString { get; }

        public abstract Task<IEnumerable<T>> QueryAsync<T>(string query, CancellationToken cancellationToken = default);
        public abstract Task<IEnumerable<T>> QueryAsync<T>(string query, DynamicParameters parameters, CancellationToken cancellationToken = default);

        public abstract Task<int> ExecuteAsync(string query, CancellationToken cancellationToken = default);
        public abstract Task<int> ExecuteAsync(string query, DynamicParameters parameters, CancellationToken cancellationToken = default);
    }
}