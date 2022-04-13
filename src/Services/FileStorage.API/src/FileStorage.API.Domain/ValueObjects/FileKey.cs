using System.Text;
using FileStorage.API.Domain.Entities;
using Library.Shared.Exceptions;
using Library.Shared.Models;

namespace FileStorage.API.Domain.ValueObjects
{
    public record FileKey : ValueObject<string>
    {
        public FileKey(string name, Folder folder)
            => Value = BuildFileKey(name, folder);

        private static string BuildFileKey(string name, Folder folder)
            => new StringBuilder()
                .Append(folder?.Key
                        ?? throw new ValidationException($"{nameof(folder)} cannot be null"))
                .Append("/")
                .Append(name)
                .ToString()
                .Replace("//", "/");
    }
}