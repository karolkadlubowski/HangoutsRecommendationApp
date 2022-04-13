using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SimpleFileSystem.Abstractions;
using SimpleFileSystem.Models;

namespace SimpleFileSystem
{
    public class FileSystemManager : IFileSystemManager
    {
        private readonly IFileSystemConfiguration _configuration;

        public FileSystemManager(IFileSystemConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<FileModel> UploadAsync(IFormFile file, string directoryPath, string fileName)
        {
            if (file == null || file.Length <= 0)
                throw new FileNotFoundException($"File '{file?.FileName}' does not exist");

            var uploadFile =
                new FileModelBuilder(_configuration.BasePath, _configuration.BaseUrl, directoryPath, file.FileName)
                    .WithFileSize(file.Length)
                    .WithFileName(fileName)
                    .Build();

            using (var stream = File.Create(uploadFile.FullPath))
            {
                await file.CopyToAsync(stream);
            }

            return uploadFile;
        }

        public async Task<FileModel> UploadAsync(IFormFile file, string directoryPath)
        {
            if (file == null || file.Length <= 0)
                throw new FileNotFoundException($"File '{file?.FileName}' does not exist");

            var uploadFile =
                new FileModelBuilder(_configuration.BasePath, _configuration.BaseUrl, directoryPath, file.FileName)
                    .WithFileSize(file.Length)
                    .Build();

            using (var stream = File.Create(uploadFile.FullPath))
            {
                await file.CopyToAsync(stream);
            }

            return uploadFile;
        }

        public async Task<IEnumerable<FileModel>> UploadAsync(IEnumerable<IFormFile> files, string directoryPath)
        {
            var uploadedTasks = files
                .Select(file => UploadAsync(file, directoryPath));

            return await Task.WhenAll(uploadedTasks);
        }

        public async Task<bool> WriteAsync(string filePath, string content)
            => await WriteAsync(filePath, content, encoding: Encoding.Default);

        public async Task<bool> WriteAsync(string filePath, string content, Encoding encoding)
        {
            var fullPath = $"{_configuration.BasePath}{filePath}";
            var directoryPath = fullPath;

            var index = directoryPath.Length - 1;
            var currentChar = directoryPath[index];
            while (currentChar != '/')
            {
                currentChar = directoryPath[index];
                directoryPath = directoryPath.Remove(index, 1);
                index--;
            }

            Directory.CreateDirectory(directoryPath);

            var encodedContent = encoding.GetBytes(content);

            using (var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None,
                       bufferSize: 4096, useAsync: true))
            {
                await stream.WriteAsync(encodedContent, 0, encodedContent.Length);
            }

            return true;
        }

        public async Task<bool> WriteJsonAsync<T>(string filePath, T content,
            JsonSerializerSettings jsonSerializerSettings = null)
            => await WriteJsonAsync(filePath, content, encoding: Encoding.Default,
                jsonSerializerSettings: jsonSerializerSettings);

        public async Task<bool> WriteJsonAsync<T>(string filePath, T content, Encoding encoding,
            JsonSerializerSettings jsonSerializerSettings = null)
        {
            var writeResult = true;

            var contentAsJson = JsonConvert.SerializeObject(content, jsonSerializerSettings);

            writeResult &= await WriteAsync(filePath, contentAsJson, encoding: encoding);

            return writeResult;
        }

        public bool CreateDirectory(string directoryPath)
        {
            var fullPath = $"{_configuration.BasePath}{directoryPath}";

            if (Directory.Exists(directoryPath))
                return false;

            Directory.CreateDirectory(fullPath);
            return true;
        }

        public string Read(string filePath)
            => File.ReadAllText($"{_configuration.BasePath}{filePath}");

        public async Task<string> ReadAsync(string filePath, Encoding encoding)
        {
            var fullPath = $"{_configuration.BasePath}{filePath}";

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"File not found under specified path: {fullPath}");

            using (var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var stringBuilder = new StringBuilder();
                var buffer = new byte[0x1000];
                var numRead = 0;
                while ((numRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    var content = encoding.GetString(buffer, 0, numRead);
                    stringBuilder.Append(content);
                }

                return stringBuilder.ToString();
            }
        }

        public async Task<T> ReadJsonAsync<T>(string filePath, JsonSerializerSettings jsonSerializerSettings = null)
            => await ReadJsonAsync<T>(filePath, encoding: Encoding.Default,
                jsonSerializerSettings: jsonSerializerSettings);

        public async Task<T> ReadJsonAsync<T>(string filePath, Encoding encoding,
            JsonSerializerSettings jsonSerializerSettings = null)
        {
            var readContent = await ReadAsync(filePath, encoding: encoding);

            return JsonConvert.DeserializeObject<T>(readContent, jsonSerializerSettings);
        }

        public bool Exists(string filePath)
            => File.Exists(filePath);

        public bool Delete(string filePath)
        {
            var fullPath = $"{_configuration.BasePath}{filePath}";

            if (!File.Exists(fullPath))
                return false;

            File.Delete(fullPath);
            return true;
        }

        public bool DeleteDirectory(string directoryPath, bool recursive = true)
        {
            var fullPath = $"{_configuration.BasePath}{directoryPath}";
            if (!Directory.Exists(fullPath))
                return false;

            Directory.Delete(fullPath, recursive: recursive);
            return true;
        }

        public bool DeleteAllFiles(IEnumerable<FileModel> fileModels)
        {
            var allDeleted = true;

            foreach (var fileModel in fileModels)
                allDeleted &= Delete(fileModel.Path);

            return allDeleted;
        }

        public async Task<byte[]> DownloadAsync(string filePath, string extension = null)
        {
            var downloadedFile = default(byte[]);

            await Task.Factory.StartNew(() =>
            {
                var fullPath = extension != null
                    ? $"{_configuration.BasePath}{filePath}.{extension}"
                    : $"{_configuration.BasePath}{filePath}";

                if (File.Exists(fullPath))
                    downloadedFile = File.ReadAllBytes(fullPath);
            });

            return downloadedFile;
        }
    }
}