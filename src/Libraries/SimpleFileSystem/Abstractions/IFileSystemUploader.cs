using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SimpleFileSystem.Models;

namespace SimpleFileSystem.Abstractions
{
    public interface IFileSystemUploader
    {
        Task<FileModel> UploadAsync(IFormFile file, string directoryPath, string fileName);
        Task<FileModel> UploadAsync(IFormFile file, string directoryPath);
        Task<IEnumerable<FileModel>> UploadAsync(IEnumerable<IFormFile> files, string directoryPath);
    }
}