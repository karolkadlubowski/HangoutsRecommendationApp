using Library.Shared.Models;

namespace VenueList.API.Domain.Entities
{
    public class Venue : RootEntity
    {
        public long VenueId { get; protected set; }
        public long UserId { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string CategoryId { get; protected set; }
        public long? CreatorId { get; protected set; }

        public static Venue Create(long venueId, long userId, string name, string description, string categoryId, long? creatorId)
            => new Venue
            {
                VenueId = venueId,
                UserId = userId,
                Name = name,
                Description = description,
                CategoryId = categoryId,
                CreatorId = creatorId
            };
    }
}