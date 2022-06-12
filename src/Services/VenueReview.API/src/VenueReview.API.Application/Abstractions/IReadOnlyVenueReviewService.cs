using System.Collections.Generic;
using System.Threading.Tasks;
using VenueReview.API.Application.Features;

namespace VenueReview.API.Application.Abstractions
{
    public interface IReadOnlyVenueReviewService
    {
        Task<IReadOnlyList<Domain.Entities.VenueReview>> GetVenueReviewsAsync(GetVenueReviewsQuery query);
    }
}