using System.Threading;
using System.Threading.Tasks;
using FileStorage.API.Domain.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FileStorage.API.Infrastructure.Database
{
    public class FileStorageDbContext
    {
        public IMongoDatabase Database { get; }

        public FileStorageDbContext(DatabaseConfig databaseConfig)
            => Database = new MongoClient(databaseConfig.DatabaseConnectionString)
                .GetDatabase(databaseConfig.DatabaseName);

        public async Task ExecuteAsync(string command, CancellationToken cancellationToken = default)
            => await Database.RunCommandAsync((Command<BsonDocument>)command, cancellationToken: cancellationToken);
    }
}