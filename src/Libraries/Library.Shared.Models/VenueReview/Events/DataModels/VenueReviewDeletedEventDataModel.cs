namespace Library.Shared.Models.VenueReview.Events.DataModels
{
    public record VenueReviewDeletedEventDataModel
    {
        public string VenueReviewId { get; init; }
    }
}