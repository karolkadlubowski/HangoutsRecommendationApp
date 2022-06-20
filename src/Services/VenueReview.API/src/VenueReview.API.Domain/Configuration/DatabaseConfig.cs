namespace VenueReview.API.Domain.Configuration
{
    public record DatabaseConfig : Library.Shared.AppConfigs.DatabaseConfig
    {
        public string DatabaseName { get; init; }
    }
}