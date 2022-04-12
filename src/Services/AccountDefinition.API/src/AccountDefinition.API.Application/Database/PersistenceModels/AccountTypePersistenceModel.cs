using System;
using Library.Shared.Models.AccountDefinition.Enums;

namespace AccountDefinition.API.Application.Database.PersistenceModels
{
    public class AccountTypePersistenceModel
    {
        public long AccountTypeId { get; init; }
        public AccountTypeOption Type { get; init; }
        public DateTime CreatedOn { get; init; }
        public DateTime? ModifiedOn { get; init; }
    }
}