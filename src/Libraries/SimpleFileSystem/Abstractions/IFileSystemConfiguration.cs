namespace SimpleFileSystem.Abstractions
{
    public interface IFileSystemConfiguration
    {
        string BasePath { get; }
        string BaseUrl { get; }
        string BaseRelativePath { get; }

        string LastDirectory { get; }

        string CombineWithBasePath(string relativePath);
        string ExtractRelativePath(string fullPath);
    }
}