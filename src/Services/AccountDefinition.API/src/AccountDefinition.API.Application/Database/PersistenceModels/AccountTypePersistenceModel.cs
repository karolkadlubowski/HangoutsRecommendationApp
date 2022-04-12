using Library.Database;
using Library.Shared.Models.AccountDefinition.Enums;

namespace AccountDefinition.API.Application.Database.PersistenceModels
{
    public record AccountTypePersistenceModel : BasePersistenceModel
    {
        public long AccountTypeId { get; init; }
        public AccountTypeOption Type { get; init; }
    }
}