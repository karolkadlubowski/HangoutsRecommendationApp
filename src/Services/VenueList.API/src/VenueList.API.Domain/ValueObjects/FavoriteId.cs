using Library.Shared.Models;

namespace VenueList.API.Domain.ValueObjects
{
    public record FavoriteId : ValueObject<string>
    {
        public FavoriteId(long venueId, long userId)
            => Value = $"venue/{venueId}/user/{userId}";
    }
}