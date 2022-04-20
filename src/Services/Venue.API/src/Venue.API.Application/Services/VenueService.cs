using System.Collections.Generic;
using System.Threading.Tasks;
using Venue.API.Application.Abstractions;
using Venue.API.Application.Features.GetVenues;

namespace Venue.API.Application.Services
{
    public class VenueService : IVenueService
    {
        public Task<IReadOnlyList<Domain.Entities.Venue>> GetVenuesAsync(GetVenuesQuery query) => throw new System.NotImplementedException();
    }
}