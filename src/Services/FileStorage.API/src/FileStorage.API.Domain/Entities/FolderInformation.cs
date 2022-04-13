using System.Collections.Generic;
using System.Linq;
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
                FolderInformationId = new GuidId(),
                Key = new FolderInformationKey(folderKey)
            };

        public void AddOrReplaceFileInformation(FileInformation fileInformation)
        {
            var file = FileInformations.FirstOrDefault(file => file.Key == fileInformation.Key);

            if (file is not null)
                FileInformations.Remove(file);
            
            FileInformations.Add(fileInformation);
        }
    }
}