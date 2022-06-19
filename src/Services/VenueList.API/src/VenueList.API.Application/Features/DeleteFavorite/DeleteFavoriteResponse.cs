using Library.Shared.Models.Response;

namespace VenueList.API.Application.Features.DeleteFavorite
{
    public record DeleteFavoriteResponse : BaseResponse
    {
        public string DeletedFavoriteId { get; init; }
        
        public DeleteFavoriteResponse(Error error = null) : base(error)
        {
        }
    }
}