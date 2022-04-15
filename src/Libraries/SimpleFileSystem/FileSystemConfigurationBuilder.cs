using System.IO;
using System.Linq;
using SimpleFileSystem.Abstractions;

namespace SimpleFileSystem
{
    public class FileSystemConfigurationBuilder : IFileSystemConfigurationBuilder
    {
        private string _basePath;
        private string _baseRelativePath;
        private string _baseUrl;

        public IFileSystemConfigurationBuilder SetBasePath(string baseRelativePath, bool treatAsAbsolutePath = false)
        {
            _baseRelativePath = baseRelativePath;
            var finalBasePath = !treatAsAbsolutePath
                ? $"{Directory.GetCurrentDirectory()}/{baseRelativePath}"
                : baseRelativePath;

            if (finalBasePath.Last() != '/' && finalBasePath.Last() != '\\')
                finalBasePath += '/';

            finalBasePath = finalBasePath.Replace(@"\", "/");

            _basePath = finalBasePath;
            return this;
        }

        public IFileSystemConfigurationBuilder SetBaseUrl(string baseUrl)
        {
            if (baseUrl.Last() != '/' && baseUrl.Last() != '\\')
                baseUrl += '/';

            _baseUrl = $"{baseUrl}{_baseRelativePath}/";
            return this;
        }

        public IFileSystemConfiguration Build() => FileSystemConfiguration.CreateInstance(_basePath, _baseUrl, _baseRelativePath);
    }
}