using System;
using AccountDefinition.API.Domain.Entities;

namespace AccountDefinition.API.Tests.Unit.Utilities.Models
{
    public class TestAccountProvider : AccountProvider
    {
        public TestAccountProvider(string provider, DateTime createdOn)
        {
            Provider = provider;
            CreatedOn = createdOn;
        }
    }
}