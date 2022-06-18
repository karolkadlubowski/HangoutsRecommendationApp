using System.Threading.Tasks;
using VenueList.API.Application.Database.PersistenceModels;

namespace VenueList.API.Application.Database.Repositories
{
    public interface IVenueRepository
    {
        Task<VenuePersistenceModel> InsertVenueAsync(Domain.Entities.Venue venue);
    }
}