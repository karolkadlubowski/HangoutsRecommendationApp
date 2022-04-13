using System.Collections.Generic;

namespace Library.Shared.Models.FileStorage.Dtos
{
    public record FolderInformationDto
    {
        public string FolderInformationId { get; init; }
        public string Key { get; init; }

        public ICollection<FileInformationDto> FileInformations { get; init; } = new HashSet<FileInformationDto>();
    }
}