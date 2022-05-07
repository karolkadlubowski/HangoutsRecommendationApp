using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Shared.Extensions;
using Library.Shared.Models.Pagination;
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

        public async Task<VenuePersistenceModel> FindVenueWithDetailsAsync(long venueId)
            => await _dbContext.Venues
                .Include(v => v.Location)
                .ThenInclude(l => l.LocationCoordinate)
                .FirstOrDefaultAsync(v => v.VenueId == venueId && !v.IsDeleted);

        public async Task<IPagedList<VenuePersistenceModel>> GetPaginatedVenuesAsync(int pageNumber, int pageSize,
            IEnumerable<long> locationsIds)
            => await _dbContext.Venues
                .Include(v => v.Location)
                .ThenInclude(l => l.LocationCoordinate)
                .Where(v => !v.IsDeleted)
                .WhereIf(locationsIds.Any(),
                    v => locationsIds.Any(locationId => v.Location.LocationId == locationId))
                .ToPagedListAsync(pageNumber, pageSize);
    }
}