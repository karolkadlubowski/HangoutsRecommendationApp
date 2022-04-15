using System.Threading.Tasks;
using FileStorage.API.Application.Abstractions;
using FileStorage.API.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using SimpleFileSystem.Abstractions;
using SimpleFileSystem.Models;

namespace FileStorage.API.Infrastructure.Adapters
{
    public class FileSystemAdapter : IFileSystemAdapter
    {
        private readonly IFileSystemManager _fileSystemManager;

        public FileSystemAdapter(IFileSystemManager fileSystemManager)
            => _fileSystemManager = fileSystemManager;

        public async Task<FileModel> UploadAsync(IFormFile file, string folderKey)
            => await _fileSystemManager.UploadAsync(file, folderKey, new FileName(file?.FileName));

        public async Task<bool> DeleteFileAsync(string fileKey)
            => await Task.Run(() => _fileSystemManager.Delete(fileKey));

        public async Task<bool> DeleteFolderAsync(string folderKey)
            => await Task.Run(() => _fileSystemManager.DeleteDirectory(folderKey));
    }
}