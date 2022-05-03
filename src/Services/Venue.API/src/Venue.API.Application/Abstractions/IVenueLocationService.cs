using System.Threading.Tasks;
using Venue.API.Application.Features.CreateVenue;

namespace Venue.API.Application.Abstractions
{
    public interface IVenueLocationService
    {
        Task<Domain.Entities.Venue> CreateVenueWithoutLocationAsync(CreateVenueCommand command, string categoryId, long creatorId);
        Task<Domain.Entities.Venue> DeleteLocationFromVenueAsync(long venueId);
    }
}