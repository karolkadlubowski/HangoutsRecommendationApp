using Library.Shared.Models;
using Venue.API.Domain.ValueObjects;

namespace Venue.API.Domain.Entities
{
    public class Venue : RootEntity
    {
        public long VenueId { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public long LocationId { get; protected set; }
        public string CategoryId { get; protected set; }
        public long? CreatorId { get; protected set; }

        public static Venue CreateDefault(string name, long locationId, string categoryId)
            => new Venue
            {
                Name = new VenueName(name),
                LocationId = locationId,
                CategoryId = new CategoryId(categoryId)
            };

        public void SetDescription(string description)
            => Description = new VenueDescription(description);

        public void CreatedBy(long creatorId)
            => CreatorId = creatorId;
    }
}