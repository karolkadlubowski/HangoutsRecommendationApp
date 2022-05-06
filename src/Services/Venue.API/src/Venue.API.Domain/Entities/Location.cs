using Library.Shared.Models;
using Venue.API.Domain.ValueObjects;

namespace Venue.API.Domain.Entities
{
    public class Location : RootEntity
    {
        public long LocationId { get; protected set; }
        public string Address { get; protected set; }

        public LocationCoordinate LocationCoordinate { get; protected set; }

        public static Location Create(string address)
            => new Location
            {
                Address = new LocationAddress(address)
            };

        public void SetCoordinates(double latitude, double longitude)
            => LocationCoordinate = LocationCoordinate.Create(LocationId, latitude, longitude);
    }
}