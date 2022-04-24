using Library.Shared.Models;

namespace Venue.API.Domain.ValueObjects
{
    public record PhotosFolderKey : ValueObject<string>
    {
        public PhotosFolderKey(long venueId)
            => Value = $"VENUES/{venueId}";
    }
}