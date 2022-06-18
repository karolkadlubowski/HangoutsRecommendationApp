using System;
using System.Threading.Tasks;
using VenueList.API.Application.Database.PersistenceModels;
using VenueList.API.Application.Database.Repositories;

namespace VenueList.API.Infrastructure.Database.Repositories
{
    public class FavoriteRepository : BaseDbRepository<FavoritePersistenceModel>, IFavoriteRepository
    {
        public FavoriteRepository(VenueListDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<FavoritePersistenceModel> InsertFavoriteAsync(Domain.Entities.Favorite favorite)
        {
            try
            {
                var venuePersistenceModel = new FavoritePersistenceModel
                {
                    VenueId = favorite.VenueId,
                    UserId = favorite.UserId,
                    Name = favorite.Name,
                    Description = favorite.Description,
                    CategoryId = favorite.CategoryId,
                    CreatorId = favorite.CreatorId
                };

                await _collection.InsertOneAsync(venuePersistenceModel);

                return venuePersistenceModel;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}