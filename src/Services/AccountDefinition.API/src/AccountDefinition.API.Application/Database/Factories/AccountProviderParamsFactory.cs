using AccountDefinition.API.Domain.ValueObjects;
using Dapper;
using Library.Database;

namespace AccountDefinition.API.Application.Database.Factories
{
    public static class AccountProviderParamsFactory
    {
        public static DynamicParameters InsertAccountProviderParams(string provider)
            => new DynamicParametersBuilder()
                .Append("@Provider", new ProviderName(provider).Value)
                .Build();

        public static DynamicParameters DeleteAccountProviderByIdParams(long accountProviderId)
            => new DynamicParametersBuilder()
                .Append("@AccountProviderId", accountProviderId)
                .Build();
    }
}