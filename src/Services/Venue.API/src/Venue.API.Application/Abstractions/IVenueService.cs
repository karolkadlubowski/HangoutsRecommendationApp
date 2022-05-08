using System.Threading.Tasks;
using Venue.API.Application.Features.CreateVenue;
using Venue.API.Application.Features.DeleteVenue;
using Venue.API.Application.Features.UpdateVenue;

namespace Venue.API.Application.Abstractions
{
    public interface IVenueService
    {
        Task<Domain.Entities.Venue> CreateVenueAsync(CreateVenueCommand command, string categoryId, long creatorId);
        Task<Domain.Entities.Venue> UpdateVenueAsync(UpdateVenueCommand command, string categoryId, long userId);
        Task<Domain.Entities.Venue> DeleteVenueAsync(DeleteVenueCommand command);
    }
}