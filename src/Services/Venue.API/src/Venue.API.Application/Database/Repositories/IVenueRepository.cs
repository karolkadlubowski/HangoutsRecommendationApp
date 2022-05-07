using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Shared.Models.Pagination;
using Venue.API.Application.Database.PersistenceModels;

namespace Venue.API.Application.Database.Repositories
{
    public interface IVenueRepository : IGenericRepository<VenuePersistenceModel>
    {
        Task<VenuePersistenceModel> FindVenueWithDetailsAsync(long venueId);

        Task<IPagedList<VenuePersistenceModel>> GetPaginatedVenuesAsync(int pageNumber, int pageSize,
            IEnumerable<long> locationsIds);
    }
}