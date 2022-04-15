using FileStorage.API.Domain.Entities;

namespace FileStorage.API.Tests.Unit.Utilities.Models
{
    public class TestFolder : Folder
    {
        public TestFolder(string key) => Key = key;
    }
}