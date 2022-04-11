using System;
using Dapper;
using Library.Database;

namespace AccountDefinition.API.Application.Database.Factories
{
    public static class AccountProviderParamsFactory
    {
        public static DynamicParameters InsertAccountProviderParams(string provider, DateTime createdOn)
            => new DynamicParametersBuilder()
                .Append("@Provider", provider)
                .Append("@CreatedOn", createdOn)
                .Build();

        public static DynamicParameters DeleteAccountProviderByIdParams(long accountProviderId)
            => new DynamicParametersBuilder()
                .Append("@AccountProviderId", accountProviderId)
                .Build();
    }
}