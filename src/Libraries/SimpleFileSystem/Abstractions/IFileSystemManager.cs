namespace SimpleFileSystem.Abstractions
{
    public interface IFileSystemManager :
        IFileSystemUploader,
        IFileSystemWriter,
        IFileSystemReader,
        IFileSystemRemoval,
        IFileDownloader
    {
    }
}