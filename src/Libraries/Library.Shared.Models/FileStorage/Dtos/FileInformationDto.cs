namespace Library.Shared.Models.FileStorage.Dtos
{
    public record FileInformationDto
    {
        public string FileInformationId { get; init; }
        public string Key { get; init; }
        public string Name { get; init; }
        public string BranchId { get; init; }

        public BranchDto Branch { get; init; }
    }
}