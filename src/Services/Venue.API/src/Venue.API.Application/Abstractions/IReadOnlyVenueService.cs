using System.Collections.Generic;
using System.Threading.Tasks;
using Venue.API.Application.Features.GetVenues;

namespace Venue.API.Application.Abstractions
{
    public interface IReadOnlyVenueService
    {
        Task<IReadOnlyList<Domain.Entities.Venue>> GetVenuesAsync(GetVenuesQuery query);
    }
}