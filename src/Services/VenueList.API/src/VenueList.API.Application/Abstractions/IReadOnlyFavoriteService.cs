using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Shared.Models.Pagination;
using Library.Shared.Models.Pagination.Models;
using VenueList.API.Application.Features.GetFavorites;

namespace VenueList.API.Application.Abstractions
{
    public interface IReadOnlyFavoriteService
    {
        Task<PaginationTuple<Domain.Entities.Favorite>> GetFavoritesAsync(GetFavoritesQuery query);
    }
}