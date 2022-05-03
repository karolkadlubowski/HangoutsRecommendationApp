using Library.Database;
using Library.Shared.Models.AccountDefinition.Enums;

namespace AccountDefinition.API.Application.Database.PersistenceModels
{
    public class AccountTypePersistenceModel : BasePersistenceModel
    {
        public long AccountTypeId { get; set; }
        public AccountTypeOption Type { get; set; }
    }
}