using Library.Shared.Models.Response;
using Library.Shared.Models.VenueList.Dtos;

namespace VenueList.API.Application.Features.AddFavorite
{
    public record AddFavoriteResponse : BaseResponse
    {
        public FavoriteDto AddedVenue { get; init; }

        public AddFavoriteResponse(Error error = null) : base(error)
        {
        }
    }
}