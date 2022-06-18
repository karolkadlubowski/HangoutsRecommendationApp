using System.Threading.Tasks;
using VenueList.API.Application.Features.AddVenue;

namespace VenueList.API.Application.Abstractions
{
    public interface IVenueService : IReadOnlyVenueService
    {
        Task<Domain.Entities.Venue> AddVenueAsync(AddVenueCommand command);

    }
}