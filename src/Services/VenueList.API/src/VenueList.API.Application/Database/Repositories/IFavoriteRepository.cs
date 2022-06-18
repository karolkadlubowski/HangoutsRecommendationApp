using System.Threading.Tasks;
using VenueList.API.Application.Database.PersistenceModels;

namespace VenueList.API.Application.Database.Repositories
{
    public interface IFavoriteRepository
    {
        Task<FavoritePersistenceModel> InsertFavoriteAsync(Domain.Entities.Favorite favorite);
    }
}