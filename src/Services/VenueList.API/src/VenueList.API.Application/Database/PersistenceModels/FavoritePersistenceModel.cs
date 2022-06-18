using Library.Database;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VenueList.API.Application.Database.PersistenceModels
{
    public class FavoritePersistenceModel : BasePersistenceModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public long VenueId { get; set; }

        public long UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public long? CreatorId { get; set; }
    }
}