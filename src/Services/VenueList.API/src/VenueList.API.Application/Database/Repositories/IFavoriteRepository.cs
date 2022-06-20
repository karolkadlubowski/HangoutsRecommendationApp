using System.Threading.Tasks;
using Library.Shared.Models.Pagination;
using VenueList.API.Application.Database.PersistenceModels;
using VenueList.API.Application.Features.GetUserFavorites;

namespace VenueList.API.Application.Database.Repositories
{
    public interface IFavoriteRepository
    {
        Task<IPagedList<FavoritePersistenceModel>> GetPaginatedFavoritesAsync(GetUserFavoritesQuery query, long userId);

        Task<FavoritePersistenceModel> InsertFavoriteAsync(Domain.Entities.Favorite favorite);

        Task<bool> DeleteFavoriteByVenueIdAsync(long venueId);

        Task<bool> DeleteFavoriteByVenueIdAndUserIdAsync(long venueId, long userId);

        Task<bool> AnyFavoriteExistsAsync(long venueId, long userId);
    }
}