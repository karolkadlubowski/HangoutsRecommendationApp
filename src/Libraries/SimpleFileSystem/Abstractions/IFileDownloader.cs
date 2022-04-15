using System.Threading.Tasks;

namespace SimpleFileSystem.Abstractions
{
    public interface IFileDownloader
    {
        Task<byte[]> DownloadAsync(string filePath, string extension = null);
    }
}