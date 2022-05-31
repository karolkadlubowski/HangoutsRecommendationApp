using System;

namespace VenueReview.API.Tests.Unit.Utilities.Models
{
    public class StubVenueReview : API.Domain.Entities.VenueReview
    {
        public StubVenueReview(
            string venueReviewId,
            long venueId,
            string content,
            long creatorId,
            double rating,
            DateTime createdOn
        )
        {
            VenueReviewId = venueReviewId;
            VenueId = venueId;
            Content = content;
            CreatorId = creatorId;
            Rating = rating;
            CreatedOn = createdOn;
        }
    }
}