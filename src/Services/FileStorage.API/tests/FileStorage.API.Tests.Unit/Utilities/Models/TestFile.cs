using FileStorage.API.Domain.Entities;
using FileStorage.API.Domain.ValueObjects;

namespace FileStorage.API.Tests.Unit.Utilities.Models
{
    public class TestFile : File
    {
        public TestFile(string key)
        {
            FileId = new GuidIdentifier();
            Key = key;
        }

        public void SetName(string name) => Name = name;
    }
}