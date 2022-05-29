using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VenueReview.API.Application.Features.AddVenueReview;

namespace VenueReview.API.Application.Abstractions
{
    public interface IVenueReviewService : IReadOnlyVenueReviewService
    {
        Task<Domain.Entities.VenueReview> AddVenueReviewAsync(AddVenueReviewCommand command);

        //Task<String> DeleteVenueReviewAsync(DeleteVenueReviewCommand command);
    }
}