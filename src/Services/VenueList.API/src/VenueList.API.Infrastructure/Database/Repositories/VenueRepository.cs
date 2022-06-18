using System;
using System.Threading.Tasks;
using VenueList.API.Application.Database.PersistenceModels;
using VenueList.API.Application.Database.Repositories;

namespace VenueList.API.Infrastructure.Database.Repositories
{
    public class VenueRepository : BaseDbRepository<VenuePersistenceModel>, IVenueRepository
    {
        public VenueRepository(VenueListDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<VenuePersistenceModel> InsertVenueAsync(Domain.Entities.Venue venue)
        {
            try
            {
                var venuePersistenceModel = new VenuePersistenceModel
                {
                    VenueId = venue.VenueId,
                    UserId = venue.UserId,
                    Name = venue.Name,
                    Description = venue.Description,
                    CategoryId = venue.CategoryId,
                    CreatorId = venue.CreatorId
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