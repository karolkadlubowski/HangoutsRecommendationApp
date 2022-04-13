namespace FileStorage.API.Domain.Configuration
{
    public record FileSystemConfig
    {
        public string FileServerUrl { get; init; } = "http://localhost:8050";
    }
}