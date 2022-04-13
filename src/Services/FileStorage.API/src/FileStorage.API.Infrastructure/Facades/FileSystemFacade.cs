using System.Threading.Tasks;
using FileStorage.API.Application.Abstractions;
using FileStorage.API.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using SimpleFileSystem.Abstractions;
using SimpleFileSystem.Models;

namespace FileStorage.API.Infrastructure.Facades
{
    public class FileSystemFacade : IFileSystemFacade
    {
        private readonly IFileSystemManager _fileSystemManager;

        public FileSystemFacade(IFileSystemManager fileSystemManager)
            => _fileSystemManager = fileSystemManager;

        public async Task<FileModel> UploadAsync(IFormFile file, string folderKey)
            => await _fileSystemManager.UploadAsync(file, folderKey, new FileName(file?.FileName));

        public async Task DeleteFileAsync(string fileKey)
            => await Task.Factory.StartNew(() => _fileSystemManager.Delete(fileKey));

        public async Task DeleteFolderAsync(string folderKey)
            => await Task.Factory.StartNew(() => _fileSystemManager.DeleteDirectory(folderKey));
    }
}