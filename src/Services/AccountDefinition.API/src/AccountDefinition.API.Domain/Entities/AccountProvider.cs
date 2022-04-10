using AccountDefinition.API.Domain.ValueObjects;
using Library.Shared.Models;

namespace AccountDefinition.API.Domain.Entities
{
    public class AccountProvider : RootEntity
    {
        public long AccountProviderId { get; protected set; }
        public string Provider { get; protected set; }

        public static AccountProvider Create(string provider)
            => new AccountProvider
            {
                Provider = new ProviderName(provider)
            };
    }
}