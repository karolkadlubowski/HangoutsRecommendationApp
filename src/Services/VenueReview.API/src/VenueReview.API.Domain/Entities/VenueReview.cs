using Library.Shared.Models;
using VenueReview.API.Domain.ValueObjects;

namespace VenueReview.API.Domain.Entities
{
    public class VenueReview : RootEntity
    {
        public string VenueReviewId { get; protected set; }
        public long VenueId { get; protected set; }
        public string Content { get; protected set; }
        public long CreatorId { get; protected set; }
        public double Rating { get; protected set; }

        public static VenueReview Create(long venueId, string content, long creatorId, double rating)
            => new VenueReview
            {
                VenueId = venueId,
                Content = new ReviewContent(content),
                CreatorId = creatorId,
                Rating = new ReviewRating(rating)
            };
    }
}