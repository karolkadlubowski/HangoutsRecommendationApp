using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using VenueReview.API.Application.Database.PersistenceModels;
using VenueReview.API.Application.Database.Repositories;

namespace VenueReview.API.Infrastructure.Database.Repositories
{
    public class VenueReviewRepository : BaseDbRepository<VenueReviewPersistenceModel>, IVenueReviewRepository
    {
        public VenueReviewRepository(VenueReviewDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<VenueReviewPersistenceModel>> GetVenueReviewsAsync(long venueId)
            => await _collection.AsQueryable()
                .Where(v => v.VenueId == venueId)
                .OrderByDescending(v => v.ModifiedOn)
                .ThenByDescending(v => v.CreatedOn)
                .ToListAsync();

        public Task<VenueReviewPersistenceModel> InsertVenueReviewAsync(Domain.Entities.VenueReview venueReview)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteVenueReviewAsync(string venueReviewId)
        {
            throw new System.NotImplementedException();
        }
    }
}