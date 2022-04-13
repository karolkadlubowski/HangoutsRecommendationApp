using Library.Shared.Exceptions;
using Library.Shared.Models;

namespace FileStorage.API.Domain.ValueObjects
{
    public record FolderKey : ValueObject<string>
    {
        public FolderKey(string folderKey)
            => Value = string.IsNullOrWhiteSpace(folderKey)
                ? throw new ValidationException($"{nameof(folderKey)} cannot be null or empty")
                : folderKey
                    .Trim()
                    .ToUpperInvariant();
    }
}