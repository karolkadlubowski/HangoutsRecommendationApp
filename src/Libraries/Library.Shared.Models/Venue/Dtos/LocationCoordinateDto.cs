namespace Library.Shared.Models.Venue.Dtos
{
    public record LocationCoordinateDto
    {
        public long LocationCoordinateId { get; init; }
        public long LocationId { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
    }
}