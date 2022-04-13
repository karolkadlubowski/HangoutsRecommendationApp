using Library.Database;

namespace FileStorage.API.Application.Database.PersistenceModels
{
    public record FilePersistenceModel : BasePersistenceModel
    {
        public string FileId { get; init; }
        public string Key { get; init; }
        public string Name { get; init; }
        public string FolderKey { get; init; }
    }
}