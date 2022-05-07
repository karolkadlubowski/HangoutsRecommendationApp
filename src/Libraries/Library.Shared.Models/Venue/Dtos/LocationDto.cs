namespace Library.Shared.Models.Venue.Dtos
{
    public record LocationDto
    {
        public long LocationId { get; init; }
        public long VenueId { get; init; }
        public string Address { get; init; }

        public LocationCoordinateDto LocationCoordinate { get; init; }
    }
}