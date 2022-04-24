namespace Venue.API.Domain.Configuration
{
    public record RestClientConfig
    {
        public string BaseApiUrl { get; init; }
    }
}