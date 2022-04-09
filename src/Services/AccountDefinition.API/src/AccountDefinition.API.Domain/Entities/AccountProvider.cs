using Library.Shared.Models;
using Library.Shared.Models.AccountDefinition.Enums;

namespace AccountDefinition.API.Domain.Entities
{
    public class AccountProvider : RootEntity
    {
        public long AccountProviderId { get; protected set; }
        public AccountProviderOption Provider { get; protected set; } = AccountProviderOption.Internal;

        public static AccountProvider Create(AccountProviderOption provider)
            => new AccountProvider { Provider = provider };

        public string ProviderName => Provider.ToString().ToUpperInvariant();
    }
}