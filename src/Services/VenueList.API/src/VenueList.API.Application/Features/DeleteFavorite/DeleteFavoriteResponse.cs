using Library.Shared.Models.Response;

namespace VenueList.API.Application.Features.DeleteFavorite
{
    public record DeleteFavoriteResponse : BaseResponse
    {
        public long DeletedFavoriteId { get; init; }
        
        public DeleteFavoriteResponse(Error error = null) : base(error)
        {
        }
    }
}