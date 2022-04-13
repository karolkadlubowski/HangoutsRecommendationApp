using System;
using System.Collections.Generic;
using FileStorage.API.Domain.ValueObjects;
using Library.Shared.Models;

namespace FileStorage.API.Domain.Entities
{
    public class FolderInformation : RootEntity
    {
        public string FolderInformationId { get; protected set; }
        public string Key { get; protected set; }

        public ICollection<FileInformation> FileInformations { get; protected set; } = new HashSet<FileInformation>();

        public static FolderInformation CreateDefault(string folderKey)
            => new FolderInformation
            {
                FolderInformationId = Guid.NewGuid().ToString(),
                Key = new FolderInformationKey(folderKey)
            };
    }
}