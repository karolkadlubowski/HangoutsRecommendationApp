using Library.Shared.Models.Response;

namespace Venue.API.Application.Handlers.DeleteCategoryFromCache
{
    public record DeleteCategoryFromCacheResponse : BaseResponse
    {
        public string CategoryId { get; init; }

        public DeleteCategoryFromCacheResponse(Error error = null) : base(error)
        {
        }
    }
}