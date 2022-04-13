using FileStorage.API.Domain.ValueObjects;
using Library.Shared.Models;

namespace FileStorage.API.Domain.Entities
{
    public class FileInformation : RootEntity
    {
        public string FileInformationId { get; protected set; }
        public string Key { get; protected set; }
        public string Name { get; protected set; }

        public static FileInformation CreateDefault(string name, FolderInformation folderInformation)
        {
            var fileInformation = new FileInformation
            {
                FileInformationId = new GuidId(), 
                Name = new FileInformationName(name)
            };

            fileInformation.Key = new FileInformationKey(fileInformation.Name, folderInformation);

            return fileInformation;
        }
    }
}