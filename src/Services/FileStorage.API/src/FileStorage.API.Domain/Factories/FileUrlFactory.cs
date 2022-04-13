using System.IO;

namespace FileStorage.API.Domain.Factories
{
    public static class FileUrlFactory
    {
        public static string CombineUrl(string baseUrl, string fileKey)
            => Path.Combine(baseUrl, fileKey);
    }
}