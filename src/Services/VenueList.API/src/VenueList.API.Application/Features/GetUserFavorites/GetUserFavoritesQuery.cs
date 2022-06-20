using Library.Shared.Models.Pagination;
using MediatR;

namespace VenueList.API.Application.Features.GetUserFavorites
{
    public record GetUserFavoritesQuery : PaginationRequestDecorator, IRequest<GetUserFavoritesResponse>
    {
    }
}