using Library.Shared.Models;

namespace VenueList.API.Domain.Entities
{
    public class Favorite : RootEntity
    {
        public string FavoriteId { get; protected set; }
        public long VenueId { get; protected set; }
        public long UserId { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string CategoryName { get; protected set; }
        public long? CreatorId { get; protected set; }

        public static Favorite Create(long venueId, long userId, string name, string description, string categoryId, long? creatorId)
            => new Favorite
            {
                VenueId = venueId,
                UserId = userId,
                Name = name,
                Description = description,
                CategoryName = categoryId,
                CreatorId = creatorId
            };
    }
}