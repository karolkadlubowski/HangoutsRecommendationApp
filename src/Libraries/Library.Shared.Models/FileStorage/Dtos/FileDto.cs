namespace Library.Shared.Models.FileStorage.Dtos
{
    public record FileDto
    {
        public string FileId { get; init; }
        public string Key { get; init; }
        public string Name { get; init; }
    }
}