using System.IO;
using SimpleFileSystem.Abstractions;

namespace FileStorage.API.Application.Factories
{
    public static class FileUrlFactory
    {
        public static string CombineUrl(IFileSystemConfiguration fileSystemConfiguration, string fileKey)
            => Path.Combine(fileSystemConfiguration.BaseUrl, fileKey);
    }
}