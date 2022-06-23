using System.Collections.Generic;
using System.Collections.Immutable;
using Library.Shared.Models.Pagination;
using Library.Shared.Models.Response;
using Library.Shared.Models.VenueList.Dtos;

namespace VenueList.API.Application.Features.GetUserFavorites
{
    public record GetUserFavoritesResponse : BaseResponse
    {
        public IReadOnlyList<FavoriteDto> Favorites { get; init; } = ImmutableList<FavoriteDto>.Empty;

        public PaginationResponseDecorator Pagination { get; init; }

        public GetUserFavoritesResponse(Error error = null) : base(error)
        {
        }
    }
}