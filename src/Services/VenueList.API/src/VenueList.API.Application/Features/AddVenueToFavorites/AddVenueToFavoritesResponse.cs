using Library.Shared.Models.Response;
using Library.Shared.Models.VenueList.Dtos;

namespace VenueList.API.Application.Features.AddVenueToFavorites
{
    public record AddVenueToFavoritesResponse : BaseResponse
    {
        public FavoriteDto AddedVenue { get; init; }

        public AddVenueToFavoritesResponse(Error error = null) : base(error)
        {
        }
    }
}