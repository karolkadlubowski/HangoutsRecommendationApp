using Library.Database;

namespace Venue.API.Application.Database.PersistenceModels
{
    public class VenuePersistenceModel : BasePersistenceModel
    {
        public long VenueId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long LocationId { get; set; }
        public string CategoryId { get; set; }
        public long? CreatorId { get; set; }
    }
}