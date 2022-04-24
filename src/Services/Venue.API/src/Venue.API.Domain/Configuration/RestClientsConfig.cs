namespace Venue.API.Domain.Configuration
{
    public record RestClientsConfig
    {
        public RestClientConfig CategoryApi { get; init; }
    }
}