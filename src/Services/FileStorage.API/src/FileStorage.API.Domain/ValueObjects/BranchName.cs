using FileStorage.API.Domain.Validation;
using Library.Shared.Exceptions;
using Library.Shared.Models;

namespace FileStorage.API.Domain.ValueObjects
{
    public record BranchName : ValueObject<string>
    {
        public BranchName(string branchName)
        {
            if (string.IsNullOrWhiteSpace(branchName))
                throw new ValidationException($"{nameof(branchName)} cannot be null or empty");

            if (branchName.Length > ValidationRules.MaximumNameLength)
                throw new ValidationException($"{nameof(branchName)} cannot be longer than {ValidationRules.MaximumNameLength} characters");

            Value = branchName
                .Trim()
                .ToUpperInvariant();
        }
    }
}