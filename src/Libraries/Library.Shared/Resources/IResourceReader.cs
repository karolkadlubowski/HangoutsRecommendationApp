using System.Reflection;
using System.Threading.Tasks;

namespace Library.Shared.Resources
{
    public interface IResourceReader
    {
        Task<string> ReadResourceAsync(string resourcePath, Assembly assembly);
    }
}