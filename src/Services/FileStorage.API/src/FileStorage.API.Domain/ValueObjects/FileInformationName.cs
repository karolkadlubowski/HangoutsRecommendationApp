using Library.Shared.Exceptions;
using Library.Shared.Models;

namespace FileStorage.API.Domain.ValueObjects
{
    public record FileInformationName : ValueObject<string>
    {
        public FileInformationName(string fileName)
            => Value = string.IsNullOrWhiteSpace(fileName)
                ? throw new ValidationException($"{nameof(fileName)} cannot be empty or null")
                : fileName.Trim();
    }
}