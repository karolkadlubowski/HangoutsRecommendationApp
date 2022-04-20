namespace Venue.API.Domain.Entities.Builders
{
    public class VenueBuilder
    {
        private readonly Venue _venue;

        public VenueBuilder(string name, long locationId, string categoryId)
            => _venue = Venue.CreateDefault(name, locationId, categoryId);

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