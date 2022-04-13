using System;
using System.IO;

namespace SimpleFileSystem.Models
{
    public class FileModelBuilder
    {
        private readonly string _basePath;
        private readonly string _baseUrl;
        private readonly string _directoryPath;
        private readonly string _fileExtension;
        private long? _fileSize;
        private string _fileName;

        public FileModelBuilder(string basePath, string baseUrl, string directoryPath, string fileName)
        {
            _basePath = basePath;
            _baseUrl = baseUrl;
            _directoryPath = directoryPath;
            _fileExtension = Path.GetExtension(fileName);
        }

        public FileModelBuilder WithFileSize(long fileSize)
        {
            _fileSize = fileSize;
            return this;
        }

        public FileModelBuilder WithFileName(string fileName)
        {
            _fileName = fileName;
            return this;
        }

        public FileModel Build()
        {
            var (relativePath, fullPath) = ($"{_directoryPath}", $"{_basePath}{_directoryPath}/");
            var fileUrl = $"{_baseUrl}{_directoryPath}/";

            Directory.CreateDirectory(fullPath);

            if (string.IsNullOrWhiteSpace(_fileName))
                _fileName = $"{Guid.NewGuid().ToString("N").Substring(0, 32)}{_fileExtension}";

            relativePath += _fileName;
            fullPath += _fileName;
            fileUrl += _fileName;

            return new FileModel(relativePath, fileUrl, fullPath: fullPath, size: _fileSize);
        }
    }
}