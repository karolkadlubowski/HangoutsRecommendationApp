using Library.Database;

namespace FileStorage.API.Application.Database.PersistenceModels
{
    public record FileInformationPersistenceModel : BasePersistenceModel
    {
        public string FileInformationId { get; init; }
        public string Key { get; init; }
        public string Name { get; init; }
        public string BranchId { get; init; }

        public BranchPersistenceModel Branch { get; init; }
    }
}