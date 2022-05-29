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

        public static VenueReview Create(string content, double rating)
            => new VenueReview
            {
                Content = new ReviewContent(content),
                Rating = new ReviewRating(rating)
            };
    }
}