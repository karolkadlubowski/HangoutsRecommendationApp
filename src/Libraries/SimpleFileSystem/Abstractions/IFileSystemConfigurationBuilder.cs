namespace SimpleFileSystem.Abstractions
{
    public interface IFileSystemConfigurationBuilder
    {
        IFileSystemConfigurationBuilder SetBasePath(string baseRelativePath, bool treatAsAbsolutePath = false);
        IFileSystemConfigurationBuilder SetBaseUrl(string baseUrl);

        IFileSystemConfiguration Build();
    }
}