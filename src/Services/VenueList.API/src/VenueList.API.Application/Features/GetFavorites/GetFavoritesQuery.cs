using System.Collections.Generic;
using Library.Shared.Models.Pagination;
using MediatR;

namespace VenueList.API.Application.Features.GetFavorites
{
    public record GetFavoritesQuery : PaginationRequestDecorator, IRequest<GetFavoritesResponse>
    {
        public long UserId { get; init; }
    }
}