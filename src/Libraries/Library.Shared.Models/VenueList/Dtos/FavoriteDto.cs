namespace Library.Shared.Models.VenueList.Dtos
{
    public record FavoriteDto
    {
        public long VenueId { get; init; }
        public long UserId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string CategoryId { get; init; }
        public long? CreatorId { get; init; }
    }
}