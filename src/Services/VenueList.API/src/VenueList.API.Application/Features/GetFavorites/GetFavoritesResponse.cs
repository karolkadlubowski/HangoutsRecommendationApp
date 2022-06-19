using System.Collections.Generic;
using System.Collections.Immutable;
using Library.Shared.Models.Pagination;
using Library.Shared.Models.Response;
using Library.Shared.Models.VenueList.Dtos;

namespace VenueList.API.Application.Features.GetFavorites
{
    public record GetFavoritesResponse : BaseResponse
    {
        public IReadOnlyList<FavoriteDto> Favorites { get; init; } = ImmutableList<FavoriteDto>.Empty;
        
        public PaginationResponseDecorator Pagination { get; init; }

        public GetFavoritesResponse(Error error = null) : base(error)
        {
            
        }

    }
}