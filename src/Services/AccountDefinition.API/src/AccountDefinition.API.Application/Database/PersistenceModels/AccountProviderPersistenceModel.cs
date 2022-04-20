using Library.Database;

namespace AccountDefinition.API.Application.Database.PersistenceModels
{
    public class AccountProviderPersistenceModel : BasePersistenceModel
    {
        public long AccountProviderId { get; set; }
        public string Provider { get; set; }
    }
}