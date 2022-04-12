using Library.Database;

namespace AccountDefinition.API.Application.Database.PersistenceModels
{
    public record AccountProviderPersistenceModel : BasePersistenceModel
    {
        public long AccountProviderId { get; init; }
        public string Provider { get; init; }
    }
}