using FileStorage.API.Domain.Validation;
using Library.Shared.Exceptions;
using Library.Shared.Models;

namespace FileStorage.API.Domain.ValueObjects
{
    public record FolderInformationKey : ValueObject<string>
    {
        public FolderInformationKey(string folderName)
        {
            if (string.IsNullOrWhiteSpace(folderName))
                throw new ValidationException($"{nameof(folderName)} cannot be null or empty");

            if (folderName.Length > ValidationRules.MaximumNameLength)
                throw new ValidationException($"{nameof(folderName)} cannot be longer than {ValidationRules.MaximumNameLength} characters");

            Value = folderName
                .Trim()
                .ToUpperInvariant();
        }
    }
}