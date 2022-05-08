using Library.Database;

namespace Venue.API.Application.Database.PersistenceModels
{
    public class LocationPersistenceModel : BasePersistenceModel
    {
        public long LocationId { get; set; }
        public long VenueId { get; set; }
        public string Address { get; set; }

        public virtual LocationCoordinatePersistenceModel LocationCoordinate { get; set; }
        public virtual VenuePersistenceModel Venue { get; set; }
    }
}