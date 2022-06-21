using System;

namespace Library.Shared.Models.VenueList.Events.DataModels
{
    public record VenueAddedToFavoritesEventDataModel
    {
        public long VenueId { get; init; }
        public long UserId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string CategoryName { get; init; }
        public long? CreatorId { get; init; }
        public DateTime CreatedOn { get; init; }
        public DateTime? ModifiedOn { get; init; }
    }
}