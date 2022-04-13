using System;
using System.Collections.Generic;
using System.Linq;
using FileStorage.API.Domain.ValueObjects;
using Library.Shared.Models;

namespace FileStorage.API.Domain.Entities
{
    public class Folder : RootEntity
    {
        public string FolderId { get; protected set; }
        public string Key { get; protected set; }

        public ICollection<File> Files { get; protected set; } = new HashSet<File>();

        public static Folder CreateDefault(string folderKey)
            => new Folder
            {
                FolderId = new GuidId(),
                Key = new FolderKey(folderKey)
            };

        public void AddOrReplaceFile(File file)
        {
            var existingFile = Files.FirstOrDefault(f => f.Key == file.Key);

            if (existingFile is not null)
                Files.Remove(existingFile);

            Files.Add(file);
        }

        public File FindFileByName(string fileName)
            => Files.FirstOrDefault(f => f.Name.Equals(fileName, StringComparison.InvariantCultureIgnoreCase));
    }
}