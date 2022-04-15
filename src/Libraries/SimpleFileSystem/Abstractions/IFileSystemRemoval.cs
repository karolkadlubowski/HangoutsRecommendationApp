using System.Collections.Generic;
using SimpleFileSystem.Models;

namespace SimpleFileSystem.Abstractions
{
    public interface IFileSystemRemoval
    {
        bool Delete(string filePath);
        bool DeleteDirectory(string directoryPath, bool recursive = true);
        bool DeleteAllFiles(IEnumerable<FileModel> fileModels);
    }
}