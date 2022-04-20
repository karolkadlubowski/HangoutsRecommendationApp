using System.Threading.Tasks;
using Library.Shared.Models.Pagination;
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

        public Task<IPagedList<VenuePersistenceModel>> GetPaginatedVenuesAsync(GetVenuesQuery query) => throw new System.NotImplementedException();
    }
}