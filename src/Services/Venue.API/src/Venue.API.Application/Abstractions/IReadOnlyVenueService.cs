using System.Threading.Tasks;
using Library.Shared.Models.Pagination;
using Library.Shared.Models.Pagination.Models;
using Venue.API.Application.Features.GetVenue;
using Venue.API.Application.Features.GetVenues;

namespace Venue.API.Application.Abstractions
{
    public interface IReadOnlyVenueService
    {
        Task<Domain.Entities.Venue> GetVenueWithPhotosAsync(GetVenueQuery query);
        Task<PaginationTuple<Domain.Entities.Venue>> GetVenuesAsync(GetVenuesQuery query);
    }
}