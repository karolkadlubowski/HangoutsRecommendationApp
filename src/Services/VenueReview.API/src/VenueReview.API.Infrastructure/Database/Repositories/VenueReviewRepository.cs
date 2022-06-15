using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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

        public async Task<VenueReviewPersistenceModel> InsertVenueReviewAsync(Domain.Entities.VenueReview venueReview)
        {
            try
            {
                var venueReviewPersistenceModel = new VenueReviewPersistenceModel
                {
                    VenueId = venueReview.VenueId,
                    Content = venueReview.Content,
                    CreatorId = venueReview.CreatorId,
                    Rating = venueReview.Rating,
                    CreatedOn = venueReview.CreatedOn
                };

                await _collection.InsertOneAsync(venueReviewPersistenceModel);

                return venueReviewPersistenceModel;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> DeleteVenueReviewAsync(string venueReviewId)
            => (await _collection.DeleteOneAsync(v => v.VenueReviewId == venueReviewId))
                .DeletedCount > 0;

        public async Task<bool> DeleteVenueReviewsByVenueIdAsync(long venueId)
            => (await _collection.DeleteManyAsync(v => v.VenueId == venueId))
                .DeletedCount > 0;

        public async Task<bool> AnyVenueReviewExistsAsync(long creatorId, long venueId)
            => (await _collection.AsQueryable()
                .AnyAsync(v => v.VenueId == venueId && v.CreatorId == creatorId));
    }
}