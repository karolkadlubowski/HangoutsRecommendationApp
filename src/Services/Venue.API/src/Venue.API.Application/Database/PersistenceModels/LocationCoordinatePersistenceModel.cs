using Library.Database;

namespace Venue.API.Application.Database.PersistenceModels
{
    public class LocationCoordinatePersistenceModel : BasePersistenceModel
    {
        public long LocationCoordinateId { get; set; }
        public long LocationId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public virtual LocationPersistenceModel Location { get; set; }
    }
}