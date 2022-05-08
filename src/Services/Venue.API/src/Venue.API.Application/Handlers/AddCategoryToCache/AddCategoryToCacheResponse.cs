using Library.Shared.Models.Response;

namespace Venue.API.Application.Handlers.AddCategoryToCache
{
    public record AddCategoryToCacheResponse : BaseResponse
    {
        public string CategoryId { get; init; }

        public AddCategoryToCacheResponse(Error error = null) : base(error)
        {
        }
    }
}