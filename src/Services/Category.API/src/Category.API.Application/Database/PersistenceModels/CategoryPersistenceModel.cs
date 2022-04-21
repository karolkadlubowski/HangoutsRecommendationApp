using Category.API.Application.Database.Attributes;
using Library.Database;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Category.API.Application.Database.PersistenceModels
{
    [BsonCollection("Categories")]
    public class CategoryPersistenceModel : BasePersistenceModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; init; }

        public string Name { get; init; }
    }
}