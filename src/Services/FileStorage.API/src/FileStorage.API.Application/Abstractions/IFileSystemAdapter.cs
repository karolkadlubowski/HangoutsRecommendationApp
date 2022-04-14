using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SimpleFileSystem.Models;

namespace FileStorage.API.Application.Abstractions
{
    public interface IFileSystemAdapter
    {
        Task<FileModel> UploadAsync(IFormFile file, string folderKey);

        Task<bool> DeleteFileAsync(string fileKey);
        Task<bool> DeleteFolderAsync(string folderKey);
    }
}