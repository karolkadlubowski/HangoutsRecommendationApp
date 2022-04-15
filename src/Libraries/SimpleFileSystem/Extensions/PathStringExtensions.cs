namespace SimpleFileSystem.Extensions
{
    public static class PathStringExtensions
    {
        public static string CleanPath(this string value)
            => value
                .Replace("\\", "/")
                .Replace("//", "/");
    }
}