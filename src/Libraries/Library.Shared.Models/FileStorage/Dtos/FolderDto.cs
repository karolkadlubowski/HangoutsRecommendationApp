using System.Collections.Generic;

namespace Library.Shared.Models.FileStorage.Dtos
{
    public record FolderDto
    {
        public string FolderId { get; init; }
        public string Key { get; init; }

        public ICollection<FileDto> Files { get; init; } = new HashSet<FileDto>();
    }
}