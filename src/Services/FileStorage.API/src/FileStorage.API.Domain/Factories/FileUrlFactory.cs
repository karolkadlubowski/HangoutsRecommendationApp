using System.IO;

namespace FileStorage.API.Domain.Factories
{
    public static class FileUrlFactory
    {
        public static string Prepare(string baseUrl, string fileKey)
            => Path.Combine(baseUrl, fileKey)
                .Replace("\\", "/");
    }
}