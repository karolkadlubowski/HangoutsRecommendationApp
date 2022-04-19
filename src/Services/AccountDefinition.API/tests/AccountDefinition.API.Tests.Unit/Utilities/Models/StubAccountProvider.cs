using System;
using AccountDefinition.API.Domain.Entities;

namespace AccountDefinition.API.Tests.Unit.Utilities.Models
{
    public class StubAccountProvider : AccountProvider
    {
        public StubAccountProvider(string provider, DateTime createdOn)
        {
            Provider = provider;
            CreatedOn = createdOn;
        }
    }
}