using System.Threading.Tasks;
using Venue.API.Application.Features.CreateVenue;
using Venue.API.Application.Features.DeleteVenue;
using Venue.API.Application.Handlers.RollbackVenueLocationDeleting;

namespace Venue.API.Application.Abstractions
{
    public interface IVenueLocationService
    {
        Task<Domain.Entities.Venue> CreateVenueWithoutLocationAsync(CreateVenueCommand command, string categoryId, long creatorId);
        Task<Domain.Entities.Venue> DeleteLocationFromVenueAsync(DeleteVenueCommand command);
        Task<Domain.Entities.Venue> RollbackLocationDeletingAsync(RollbackVenueLocationDeletingCommand command);
    }
}