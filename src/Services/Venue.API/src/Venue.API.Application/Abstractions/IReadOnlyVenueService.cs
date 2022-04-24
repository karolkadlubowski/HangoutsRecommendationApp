using System.Threading.Tasks;
using Library.Shared.Models.Pagination;
using Library.Shared.Models.Pagination.Models;
using Venue.API.Application.Features.GetVenues;

namespace Venue.API.Application.Abstractions
{
    public interface IReadOnlyVenueService
    {
        Task<PaginationTuple<Domain.Entities.Venue>> GetVenuesAsync(GetVenuesQuery query);
    }
}