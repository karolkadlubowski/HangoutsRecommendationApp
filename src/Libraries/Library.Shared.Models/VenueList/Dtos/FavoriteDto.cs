namespace Library.Shared.Models.VenueList.Dtos
{
    public record FavoriteDto
    {
        public string FavoriteId { get; init; }
        public long VenueId { get; init; }
        public long UserId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string CategoryName { get; init; }
        public long? CreatorId { get; init; }
    }
}