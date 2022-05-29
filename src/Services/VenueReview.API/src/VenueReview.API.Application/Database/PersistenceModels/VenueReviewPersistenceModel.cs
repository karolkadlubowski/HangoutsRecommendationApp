using Library.Database;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using VenueReview.API.Application.Database.Attributes;

namespace VenueReview.API.Application.Database.PersistenceModels
{
    [BsonCollection("VenueReviews")]
    public class VenueReviewPersistenceModel : BasePersistenceModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string VenueReviewId { get; set; }
        
        public long VenueId { get; set; }

        public string Content { get;  set; }

        public long CreatorId { get;  set; }

        public double Rating { get;  set; }
    }
}