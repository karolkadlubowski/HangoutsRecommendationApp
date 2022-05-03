namespace Venue.API.Domain.Entities.Builders
{
    public class VenueBuilder
    {
        private readonly Venue _venue;

        public VenueBuilder(string name, string categoryId)
            => _venue = Venue.CreateWithoutLocation(name, categoryId);

        public VenueBuilder WithLocation(long locationId)
        {
            _venue.AssignLocation(locationId);
            return this;
        }

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

        public Venue Build() => _venue;
    }
}