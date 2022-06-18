using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using VenueList.API.Domain.Configuration;

namespace VenueList.API.Infrastructure.Database
{
    public class VenueListDbContext
    {
        public IMongoDatabase Database { get; }

        public VenueListDbContext(DatabaseConfig databaseConfig)
            => Database = new MongoClient(databaseConfig.DatabaseConnectionString)
                .GetDatabase(databaseConfig.DatabaseName);
        
        public async Task ExecuteAsync(string command, CancellationToken cancellationToken = default)
            => await Database.RunCommandAsync((Command<BsonDocument>)command, cancellationToken: cancellationToken);
    }
}