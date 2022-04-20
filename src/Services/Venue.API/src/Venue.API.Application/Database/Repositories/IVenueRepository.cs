using System.Threading.Tasks;
using Library.Shared.Models.Pagination;
using Venue.API.Application.Database.PersistenceModels;
using Venue.API.Application.Features.GetVenues;

namespace Venue.API.Application.Database.Repositories
{
    public interface IVenueRepository : IGenericRepository<VenuePersistenceModel>
    {
        Task<IPagedList<VenuePersistenceModel>> GetPaginatedVenuesAsync(GetVenuesQuery query);
    }
}