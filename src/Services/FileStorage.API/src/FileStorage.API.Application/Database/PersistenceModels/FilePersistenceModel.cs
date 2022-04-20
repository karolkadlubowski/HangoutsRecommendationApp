using Library.Database;

namespace FileStorage.API.Application.Database.PersistenceModels
{
    public class FilePersistenceModel : BasePersistenceModel
    {
        public string FileId { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string FolderKey { get; set; }
    }
}