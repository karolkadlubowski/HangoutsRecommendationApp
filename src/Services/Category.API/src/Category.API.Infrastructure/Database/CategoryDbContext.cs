using System.Threading;
using System.Threading.Tasks;
using Category.API.Domain.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Category.API.Infrastructure.Database
{
    public class CategoryDbContext
    {
        public IMongoDatabase Database { get; }

        public CategoryDbContext(DatabaseConfig databaseConfig)
            => Database = new MongoClient(databaseConfig.DatabaseConnectionString)
                .GetDatabase(databaseConfig.DatabaseName);

        public async Task ExecuteAsync(string command, CancellationToken cancellationToken = default)
            => await Database.RunCommandAsync((Command<BsonDocument>)command, cancellationToken: cancellationToken);
    }
}