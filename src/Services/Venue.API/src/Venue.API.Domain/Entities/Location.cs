using Library.Shared.Models;
using Venue.API.Domain.ValueObjects;

namespace Venue.API.Domain.Entities
{
    public class Location : RootEntity
    {
        public long LocationId { get; protected set; }
        public long VenueId { get; protected set; }
        public string Address { get; protected set; }

        public LocationCoordinate LocationCoordinate { get; protected set; }

        public static Location Create(string address)
            => new Location
            {
                Address = new LocationAddress(address)
            };

        public void Update(string address, double latitude, double longitude)
        {
            Address = new LocationAddress(address);
            UpdateCoordinates(latitude, longitude);
        }

        public void SetCoordinates(double latitude, double longitude)
            => LocationCoordinate = LocationCoordinate.Create(LocationId, latitude, longitude);

        public void UpdateCoordinates(double latitude, double longitude)
            => LocationCoordinate.UpdateCoordinates(latitude, longitude);
    }
}