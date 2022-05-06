using Library.Shared.Models;

namespace Venue.API.Domain.Entities
{
    public class LocationCoordinate : RootEntity
    {
        public long LocationCoordinateId { get; protected set; }
        public long LocationId { get; protected set; }
        public double Latitude { get; protected set; }
        public double Longitude { get; protected set; }

        public static LocationCoordinate Create(long locationId, double latitude, double longitude)
            => new LocationCoordinate
            {
                LocationId = locationId,
                Latitude = latitude,
                Longitude = longitude
            };
    }
}