using Library.Database;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using VenueList.API.Application.Database.Attributes;

namespace VenueList.API.Application.Database.PersistenceModels
{
    [BsonCollection("Favorites")]
    public class FavoritePersistenceModel : BasePersistenceModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FavoriteId { get; set; }

        public long VenueId { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public long? CreatorId { get; set; }
    }
}