using System.IO;
using Microsoft.AspNetCore.Http;

namespace FileStorage.API.Tests.Unit.Utilities.Factories
{
    public static class FormFileFactory
    {
        public static IFormFile CreateFormFileWithName(string name)
            => new FormFile(Stream.Null, default, default, name, name);
    }
}