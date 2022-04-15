using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SimpleFileSystem.Abstractions
{
    public interface IFileSystemReader
    {
        string Read(string filePath);
        Task<string> ReadAsync(string filePath, Encoding encoding);

        Task<T> ReadJsonAsync<T>(string filePath, JsonSerializerSettings jsonSerializerSettings = null);

        Task<T> ReadJsonAsync<T>(string filePath, Encoding encoding,
            JsonSerializerSettings jsonSerializerSettings = null);

        bool Exists(string filePath);
    }
}