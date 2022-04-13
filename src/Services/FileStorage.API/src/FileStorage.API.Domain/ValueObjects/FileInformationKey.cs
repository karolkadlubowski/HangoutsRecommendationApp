using System.Text;
using FileStorage.API.Domain.Entities;
using Library.Shared.Exceptions;
using Library.Shared.Models;

namespace FileStorage.API.Domain.ValueObjects
{
    public record FileInformationKey : ValueObject<string>
    {
        public FileInformationKey(string name, FolderInformation folderInformation)
            => Value = BuildFileInformationKey(name, folderInformation);

        private static string BuildFileInformationKey(string name, FolderInformation folderInformation)
            => new StringBuilder()
                .Append(folderInformation?.Key
                        ?? throw new ValidationException($"{nameof(folderInformation)} cannot be null"))
                .Append("/")
                .Append(name)
                .ToString()
                .Replace("//", "/");
    }
}