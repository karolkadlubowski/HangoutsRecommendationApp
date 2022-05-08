using MediatR;

namespace Venue.API.Application.Handlers.DeleteCategoryFromCache
{
    public record DeleteCategoryFromCacheCommand
    (
        string CategoryId
    ) : IRequest<DeleteCategoryFromCacheResponse>;
}