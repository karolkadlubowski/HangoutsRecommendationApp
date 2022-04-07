using System.Text.Json;

namespace Library.Shared.Options
{
    public static class JsonOptions
    {
        public static JsonSerializerOptions JsonSerializerOptions => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }
}