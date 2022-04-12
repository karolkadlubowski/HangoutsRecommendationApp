using System;
using FileStorage.API.Domain.ValueObjects;
using Library.Shared.Models;

namespace FileStorage.API.Domain.Entities
{
    public class FileInformation : RootEntity
    {
        public string FileInformationId { get; protected set; }
        public string Key { get; protected set; }
        public string Name { get; protected set; }
        public string BranchId { get; protected set; }

        public Branch Branch { get; protected set; }

        public static FileInformation CreateDefault(string name, Branch branch)
        {
            var fileInformation = new FileInformation
            {
                FileInformationId = Guid.NewGuid().ToString(),
                Name = new FileInformationName(name),
                Branch = branch
            };

            fileInformation.Key = new FileInformationKey(fileInformation.Name, branch);
            fileInformation.BranchId = new FileInformationBranchId(fileInformation.Branch);

            return fileInformation;
        }
    }
}