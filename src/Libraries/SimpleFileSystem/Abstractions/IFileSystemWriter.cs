using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SimpleFileSystem.Abstractions
{
    public interface IFileSystemWriter
    {
        Task<bool> WriteAsync(string filePath, string content);
        Task<bool> WriteAsync(string filePath, string content, Encoding encoding);

        Task<bool> WriteJsonAsync<T>(string filePath, T content, JsonSerializerSettings jsonSerializerSettings = null);

        Task<bool> WriteJsonAsync<T>(string filePath, T content, Encoding encoding,
            JsonSerializerSettings jsonSerializerSettings = null);

        bool CreateDirectory(string directoryPath);
    }
}