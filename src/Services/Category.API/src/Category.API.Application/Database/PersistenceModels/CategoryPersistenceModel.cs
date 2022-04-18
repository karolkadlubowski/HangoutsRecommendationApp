using Category.API.Application.Database.Attributes;
using Library.Database;
using MongoDB.Bson.Serialization.Attributes;

namespace Category.API.Application.Database.PersistenceModels
{
    [BsonCollection("Categories")]
    public record CategoryPersistenceModel : BasePersistenceModel
    {
        [BsonId]
        public string CategoryId { get; init; }

        public string Name { get; init; }
    }
}