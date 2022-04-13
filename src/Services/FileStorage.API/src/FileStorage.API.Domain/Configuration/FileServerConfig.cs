namespace FileStorage.API.Domain.Configuration
{
    public record FileServerConfig
    {
        public string FileServerBasePath { get; init; } = "wwwroot";
        public string FileServerUrl { get; init; } = "http://localhost:8050";
    }
}