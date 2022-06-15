using Library.Shared.Models.Venue.Enums;

namespace Venue.API.Domain.Entities.Builders
{
    public class VenueBuilder
    {
        private readonly Venue _venue;

        public VenueBuilder(string name, string categoryId, VenueStyle style, VenueOccupancy occupancy)
            => _venue = Venue.CreateDefault(name, categoryId, style, occupancy);

        public VenueBuilder WithDescription(string description)
        {
            _venue.SetDescription(description);
            return this;
        }

        public VenueBuilder CreatedBy(long creatorId)
        {
            _venue.CreatedBy(creatorId);
            return this;
        }

        public VenueBuilder WithLocation(string address, double latitude, double longitude)
        {
            _venue.SetLocationWithCoordinates(address, latitude, longitude);
            return this;
        }

        public Venue Build() => _venue;
    }
}