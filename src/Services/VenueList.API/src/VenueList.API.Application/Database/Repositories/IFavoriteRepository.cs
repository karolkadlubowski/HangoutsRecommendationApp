using System.Threading.Tasks;
using Library.Shared.Models.Pagination;
using VenueList.API.Application.Database.PersistenceModels;
using VenueList.API.Application.Features.GetFavorites;

namespace VenueList.API.Application.Database.Repositories
{
    public interface IFavoriteRepository
    {
        Task<FavoritePersistenceModel> InsertFavoriteAsync(Domain.Entities.Favorite favorite);
        Task<bool> DeleteFavoriteAsync(string venueReviewId);
        
        Task<bool> AnyFavoriteExistsAsync(long venueId, long userId);
        
        Task<IPagedList<FavoritePersistenceModel>> GetPaginatedFavoritesAsync(GetFavoritesQuery query);

    }
}