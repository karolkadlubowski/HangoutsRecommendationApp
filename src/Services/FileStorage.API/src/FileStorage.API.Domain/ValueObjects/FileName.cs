using Library.Shared.Exceptions;
using Library.Shared.Models;

namespace FileStorage.API.Domain.ValueObjects
{
    public record FileName : ValueObject<string>
    {
        public FileName(string fileName)
            => Value = string.IsNullOrWhiteSpace(fileName)
                ? throw new ValidationException($"{nameof(fileName)} cannot be null or empty")
                : fileName.Trim();
    }
}