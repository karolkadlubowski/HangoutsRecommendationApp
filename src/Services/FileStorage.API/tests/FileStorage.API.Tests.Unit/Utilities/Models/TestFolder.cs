using FileStorage.API.Domain.Entities;
using FileStorage.API.Domain.ValueObjects;

namespace FileStorage.API.Tests.Unit.Utilities.Models
{
    public class TestFolder : Folder
    {
        public TestFolder(string key)
        {
            FolderId = new GuidIdentifier();
            Key = key;
        }
    }
}