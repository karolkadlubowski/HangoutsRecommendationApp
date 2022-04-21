using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace FileStorage.API.Tests.Unit.Utilities.Factories
{
    public static class FormFileFactory
    {
        public static IFormFile CreateFormFileWithName(string name)
        {
            var bytes = Encoding.UTF8.GetBytes(name);

            return new FormFile(new MemoryStream(bytes),
                default,
                bytes.Length,
                name,
                name)
            {
                Headers = new HeaderDictionary(),
                ContentType = "form-data"
            };
        }
    }
}