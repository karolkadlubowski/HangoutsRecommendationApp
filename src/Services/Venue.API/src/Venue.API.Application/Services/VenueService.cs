using System.Threading.Tasks;
using Library.Shared.Models.Pagination;
using Venue.API.Application.Abstractions;
using Venue.API.Application.Features.GetVenues;

namespace Venue.API.Application.Services
{
    public class VenueService : IVenueService
    {
        public Task<IPagedList<Domain.Entities.Venue>> GetVenuesAsync(GetVenuesQuery query) => throw new System.NotImplementedException();
    }
}