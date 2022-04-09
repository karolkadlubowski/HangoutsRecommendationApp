using Library.Shared.Models;
using Library.Shared.Models.AccountDefinition.Enums;

namespace AccountDefinition.API.Domain.Entities
{
    public class AccountType : RootEntity
    {
        public long AccountTypeId { get; protected set; }
        public AccountTypeOption Type { get; protected set; } = AccountTypeOption.Normal;

        public static AccountType Create(AccountTypeOption type)
            => new AccountType { Type = type };

        public string TypeName => Type.ToString().ToUpperInvariant();
    }
}