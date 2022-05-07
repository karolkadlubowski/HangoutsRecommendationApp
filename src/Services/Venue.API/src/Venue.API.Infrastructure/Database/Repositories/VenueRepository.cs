using System.Linq;
using System.Threading.Tasks;
using Library.Shared.Extensions;
using Library.Shared.Models.Pagination;
using Microsoft.EntityFrameworkCore;
using Venue.API.Application.Database.PersistenceModels;
using Venue.API.Application.Database.Repositories;
using Venue.API.Application.Features.GetVenues;

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

        public async Task<IPagedList<VenuePersistenceModel>> GetPaginatedVenuesAsync(GetVenuesQuery query)
            => await _dbContext.Venues
                .Include(v => v.Location)
                .ThenInclude(l => l.LocationCoordinate)
                .Where(v => !v.IsDeleted)
                .WhereIf(query.LocationsIds.Any(),
                    v => query.LocationsIds.Any(locationId => v.Location.LocationId == locationId))
                .WhereIf(query.CategoriesIds.Any(),
                    v => query.CategoriesIds.Any(categoryId => v.CategoryId == categoryId))
                .ToPagedListAsync(query.PageNumber, query.PageSize);
    }
}