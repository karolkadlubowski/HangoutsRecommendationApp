using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Venue.API.Application.Database.PersistenceModels;
using Venue.API.Application.Database.Repositories;

namespace Venue.API.Infrastructure.Database.Repositories
{
    public class VenueRepository : GenericRepository<VenuePersistenceModel>, IVenueRepository
    {
        public VenueRepository(VenueDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<VenuePersistenceModel>> GetVenuesByLocationsIdsAsync(IEnumerable<long> locationsIds)
            => await _dbContext.Venues
                .Where(v => locationsIds.Contains(v.LocationId))
                .ToListAsync();
    }
}