using System.Threading;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;
using VenueReview.API.Domain.Configuration;

namespace VenueReview.API.Infrastructure.Database
{
    public class VenueReviewDbContext
    {
        public IMongoDatabase Database { get; }
        
        public VenueReviewDbContext(DatabaseConfig databaseConfig)
            => Database = new MongoClient(databaseConfig.DatabaseConnectionString)
                .GetDatabase(databaseConfig.DatabaseName);

        public async Task ExecuteAsync(string command, CancellationToken cancellationToken = default)
            => await Database.RunCommandAsync((Command<BsonDocument>)command, cancellationToken: cancellationToken);
    }
}