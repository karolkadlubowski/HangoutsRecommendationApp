using System.Text;
using FileStorage.API.Domain.Configuration;
using FileStorage.API.Domain.ValueObjects;

namespace FileStorage.API.Infrastructure.Adapters.Builders
{
    public class FileUrlBuilder
    {
        private readonly StringBuilder _urlBuilder = new StringBuilder();

        public FileUrlBuilder(FileSystemConfig fileSystemConfig)
            => _urlBuilder
                .Append(fileSystemConfig.FileServerUrl)
                .Append("/");

        public FileUrlBuilder WithFolderKey(string key)
        {
            _urlBuilder
                .Append(new FolderKey(key))
                .Append("/");

            return this;
        }

        public FileUrlBuilder WithFileName(string name)
        {
            _urlBuilder
                .Append(new FileName(name));

            return this;
        }

        public string Build() => _urlBuilder.ToString();
    }
}