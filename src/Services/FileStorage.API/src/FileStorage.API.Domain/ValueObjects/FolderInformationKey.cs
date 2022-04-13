using Library.Shared.Exceptions;
using Library.Shared.Models;

namespace FileStorage.API.Domain.ValueObjects
{
    public record FolderInformationKey : ValueObject<string>
    {
        public FolderInformationKey(string folderKey)
            => Value = string.IsNullOrWhiteSpace(folderKey)
                ? throw new ValidationException($"{nameof(folderKey)} cannot be null or empty")
                : folderKey
                    .Trim()
                    .ToUpperInvariant();
    }
}