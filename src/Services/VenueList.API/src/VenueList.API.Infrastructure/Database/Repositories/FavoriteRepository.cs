using System;
using System.Threading.Tasks;
using Library.Shared.Extensions;
using Library.Shared.Models.Pagination;
using VenueList.API.Application.Database.PersistenceModels;
using VenueList.API.Application.Database.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using VenueList.API.Application.Features.GetUserFavorites;

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

        public async Task<bool> AnyFavoriteExistsAsync(long venueId, long userId)
            => (await _collection.AsQueryable()
                .AnyAsync(f => f.VenueId == venueId && f.UserId == userId));

        public async Task<IPagedList<FavoritePersistenceModel>> GetPaginatedFavoritesAsync(GetUserFavoritesQuery query, long userId)
            => await _collection.AsQueryable()
                .Where(f => f.UserId == userId)
                .OrderByDescending(v => v.ModifiedOn)
                .ThenByDescending(v => v.CreatedOn)
                .ToPagedListAsync(query.PageNumber, query.PageSize);

        public async Task<bool> DeleteFavoriteByVenueIdAsync(long venueId)
            => (await _collection.DeleteManyAsync(f => f.VenueId == venueId))
                .DeletedCount > 0;

        public async Task<bool> DeleteFavoriteByVenueIdAndUserIdAsync(long venueId, long userId)
            => (await _collection.DeleteManyAsync(f => f.VenueId == venueId && f.UserId == userId))
                .DeletedCount > 0;
    }
}