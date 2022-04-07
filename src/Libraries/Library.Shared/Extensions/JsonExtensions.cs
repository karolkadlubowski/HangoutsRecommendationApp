using System.Text.Json;

namespace Library.Shared.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJSON(this object obj, JsonSerializerOptions options = null) =>
            JsonSerializer.Serialize(obj, options: options);

        public static T FromJSON<T>(this string obj, JsonSerializerOptions options = null) =>
            JsonSerializer.Deserialize<T>(obj, options: options);
    }
}