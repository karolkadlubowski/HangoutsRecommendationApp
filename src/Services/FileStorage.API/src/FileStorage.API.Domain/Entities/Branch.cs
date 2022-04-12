using System;
using System.Collections.Generic;
using FileStorage.API.Domain.ValueObjects;
using Library.Shared.Models;

namespace FileStorage.API.Domain.Entities
{
    public class Branch : RootEntity
    {
        public string BranchId { get; protected set; }
        public string Name { get; protected set; }
        public string ParentBranchId { get; protected set; }

        public Branch ParentBranch { get; protected set; }

        public ICollection<FileInformation> FileInformations { get; protected set; } = new HashSet<FileInformation>();

        public static Branch CreateRoot(string branchName)
            => new Branch
            {
                BranchId = Guid.NewGuid().ToString(),
                Name = new BranchName(branchName)
            };

        public static Branch CreateNestedBranch(string branchName, Branch parentBranch)
            => new Branch
            {
                BranchId = Guid.NewGuid().ToString(),
                Name = new BranchName(branchName),
                ParentBranch = parentBranch,
                ParentBranchId = parentBranch?.BranchId
            };
    }
}