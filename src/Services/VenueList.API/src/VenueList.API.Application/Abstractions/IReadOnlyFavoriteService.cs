using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Shared.Models.Pagination;
using Library.Shared.Models.Pagination.Models;
using VenueList.API.Application.Features.GetUserFavorites;

namespace VenueList.API.Application.Abstractions
{
    public interface IReadOnlyFavoriteService
    {
        Task<PaginationTuple<Domain.Entities.Favorite>> GetUserFavoritesAsync(GetUserFavoritesQuery query, long userId);
    }
}