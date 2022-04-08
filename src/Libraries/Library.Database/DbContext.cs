using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Library.Database.Abstractions;

namespace Library.Database
{
    public abstract class DbContext : IDbContext
    {
        protected readonly string _connectionString;

        protected DbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public abstract Task ExecuteAsync(string query, CancellationToken cancellationToken = default);

        public abstract Task ExecuteAsync(string query, DynamicParameters parameters, CancellationToken cancellationToken = default);
    }
}