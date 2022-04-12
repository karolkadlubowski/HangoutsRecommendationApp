using System.Collections.Generic;

namespace Library.Shared.Models.FileStorage.Dtos
{
    public record BranchDto
    {
        public string BranchId { get; init; }

        public string Name { get; init; }
        public string ParentBranchId { get; init; }

        public BranchDto ParentBranch { get; init; }

        public ICollection<FileInformationDto> FileInformations { get; init; } = new HashSet<FileInformationDto>();
    }
}