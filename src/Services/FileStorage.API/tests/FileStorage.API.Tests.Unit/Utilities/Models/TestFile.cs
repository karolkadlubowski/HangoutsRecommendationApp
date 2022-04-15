using FileStorage.API.Domain.Entities;

namespace FileStorage.API.Tests.Unit.Utilities.Models
{
    public class TestFile : File
    {
        public TestFile(string key) => Key = key;

        public void SetName(string name) => Name = name;
    }
}