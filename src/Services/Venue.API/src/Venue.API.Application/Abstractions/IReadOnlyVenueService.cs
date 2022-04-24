using System.Threading.Tasks;
using Library.Shared.Models.Pagination;
using Venue.API.Application.Features.GetVenues;

namespace Venue.API.Application.Abstractions
{
    public interface IReadOnlyVenueService
    {
        Task<IPagedList<Domain.Entities.Venue>> GetVenuesAsync(GetVenuesQuery query);
    }
}