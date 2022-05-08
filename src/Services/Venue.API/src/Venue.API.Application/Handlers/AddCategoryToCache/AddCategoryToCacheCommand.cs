using MediatR;

namespace Venue.API.Application.Handlers.AddCategoryToCache
{
    public record AddCategoryToCacheCommand
    (
        string CategoryId,
        string CategoryName
    ) : IRequest<AddCategoryToCacheResponse>;
}