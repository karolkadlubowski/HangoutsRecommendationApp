using System.Collections.Generic;
using System.Threading.Tasks;
using Venue.API.Application.Database.PersistenceModels;

namespace Venue.API.Application.Database.Repositories
{
    public interface IVenueRepository : IGenericRepository<VenuePersistenceModel>
    {
        Task<IReadOnlyList<VenuePersistenceModel>> GetVenuesByLocationsIdsAsync(IEnumerable<long> locationsIds);
    }
}