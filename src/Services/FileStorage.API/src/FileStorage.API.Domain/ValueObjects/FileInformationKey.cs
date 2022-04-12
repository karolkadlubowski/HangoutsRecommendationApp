using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileStorage.API.Domain.Entities;
using Library.Shared.Models;

namespace FileStorage.API.Domain.ValueObjects
{
    public record FileInformationKey : ValueObject<string>
    {
        public FileInformationKey(string name, Branch branch)
            => Value = BuildFileInformationKey(name, branch);

        private static string BuildFileInformationKey(string name, Branch branch)
        {
            var keyBuilder = new StringBuilder();
            var branchesStack = new Stack<Branch>();
            var currentBranch = branch;

            while (currentBranch is not null)
            {
                branchesStack.Push(currentBranch);
                currentBranch = branch.ParentBranch;
            }

            while (branchesStack.Any())
                keyBuilder.Append($"{new BranchName(branchesStack.Pop().Name).Value}/");

            return keyBuilder
                .Append(name)
                .ToString();
        }
    }
}