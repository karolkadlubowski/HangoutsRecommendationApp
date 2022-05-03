namespace Venue.API.Domain.Entities.Models
{
    public record Photo
    {
        public string FileId { get; init; }
        public string Key { get; init; }
        public string Name { get; init; }
        public string FolderKey { get; init; }
        public string FileUrl { get; init; }
    }
}