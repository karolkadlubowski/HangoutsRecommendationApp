using System.Threading.Tasks;
using Venue.API.Application.Features.CreateVenue;

namespace Venue.API.Application.Abstractions
{
    public interface IVenueService : IReadOnlyVenueService
    {
        Task<Domain.Entities.Venue> CreateVenueAsync(CreateVenueCommand command, string categoryId, long creatorId);
    }
}