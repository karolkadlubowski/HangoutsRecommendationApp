using System;
using System.Linq;
using SimpleFileSystem.Abstractions;
using SimpleFileSystem.Extensions;

namespace SimpleFileSystem
{
    public class FileSystemConfiguration : IFileSystemConfiguration
    {
        public string BasePath { get; private set; }
        public string BaseUrl { get; private set; }
        public string BaseRelativePath { get; private set; }

        public string LastDirectory => BasePath
            .TrimEnd('/')
            .Split('/')
            .Last();

        public string CombineWithBasePath(string relativePath)
            => $"{BasePath}{relativePath}"
                .CleanPath();

        public string ExtractRelativePath(string fullPath)
            => fullPath
                .Substring(BasePath.Length - 1)
                .CleanPath();

        public static FileSystemConfiguration CreateInstance(string basePath, string baseUrl, string baseRelativePath)
        {
            if (string.IsNullOrWhiteSpace(basePath))
                throw new ArgumentNullException(nameof(BasePath));

            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentNullException(nameof(BaseUrl));

            if (string.IsNullOrWhiteSpace(baseRelativePath))
                throw new ArgumentNullException(nameof(BaseRelativePath));

            return new FileSystemConfiguration { BasePath = basePath, BaseUrl = baseUrl, BaseRelativePath = baseRelativePath };
        }
    }
}