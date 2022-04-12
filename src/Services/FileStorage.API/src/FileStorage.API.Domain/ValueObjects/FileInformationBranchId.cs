using System;
using FileStorage.API.Domain.Entities;
using Library.Shared.Models;

namespace FileStorage.API.Domain.ValueObjects
{
    public record FileInformationBranchId : ValueObject<string>
    {
        public FileInformationBranchId(Branch branch)
            => Value = branch is null
                ? throw new ArgumentNullException(nameof(branch))
                : branch.BranchId;
    }
}