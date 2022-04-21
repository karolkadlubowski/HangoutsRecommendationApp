using FileStorage.API.Domain.Factories;
using FileStorage.API.Domain.ValueObjects;
using Library.Shared.Models;

namespace FileStorage.API.Domain.Entities
{
    public class File : RootEntity
    {
        public string FileId { get; protected set; }
        public string Key { get; protected set; }
        public string Name { get; protected set; }
        public string FolderKey { get; protected set; }

        public string FileUrl { get; protected set; }

        public static File CreateDefault(string name, Folder folder)
        {
            var file = new File
            {
                FileId = new GuidIdentifier(),
                Name = new FileName(name)
            };

            file.Key = new FileKey(file.Name, folder);
            file.FolderKey = folder.Key;

            return file;
        }

        public void SetUrl(string baseUrl)
            => FileUrl = FileUrlFactory.Prepare(baseUrl, Key);
    }
}