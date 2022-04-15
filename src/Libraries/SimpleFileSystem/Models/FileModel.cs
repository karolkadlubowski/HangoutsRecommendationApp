namespace SimpleFileSystem.Models
{
    public class FileModel
    {
        public string Path { get; }
        public string Url { get; }
        public string FullPath { get; }
        public long? Size { get; }

        public FileModel(string path, string url, string fullPath = null, long? size = null)
        {
            Path = path;
            Url = url;
            FullPath = fullPath;
            Size = size;
        }

        public FileModel(string path)
        {
            Path = path;
            Url = null;
            FullPath = path;
            Size = null;
        }
    }
}