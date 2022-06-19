using System;
using System.Threading.Tasks;
using VenueList.API.Application.Database.PersistenceModels;
using VenueList.API.Application.Database.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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
                    CategoryName = favorite.CategoryName,
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

        public async Task<bool> DeleteFavoriteAsync(string favoriteId)
            => (await _collection.DeleteOneAsync(f => f.FavoriteId == favoriteId))
                .DeletedCount > 0;

        public async Task<bool> AnyFavoriteExistsAsync(long venueId, long userId)
            => (await _collection.AsQueryable()
                .AnyAsync(f => f.VenueId == venueId && f.UserId == userId));
    }
}