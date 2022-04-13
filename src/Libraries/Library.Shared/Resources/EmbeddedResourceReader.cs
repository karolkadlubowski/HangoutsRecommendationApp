using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Library.Shared.Resources
{
    public class EmbeddedResourceReader : IResourceReader
    {
        public async Task<string> ReadResourceAsync(string resourcePath, Assembly assembly)
        {
            var stream = assembly.GetManifestResourceStream(resourcePath)
                         ?? throw new FileNotFoundException(
                             $"Embedded resource not found under the given path: '{resourcePath}'");

            using (var streamReader = new StreamReader(stream))
            {
                return await streamReader.ReadToEndAsync();
            }
        }
    }
}