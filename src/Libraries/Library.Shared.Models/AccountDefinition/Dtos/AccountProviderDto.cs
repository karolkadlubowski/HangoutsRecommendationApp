using System;

namespace Library.Shared.Models.AccountDefinition.Dtos
{
    public record AccountProviderDto
    {
        public long AccountProviderId { get; init; }
        public string Provider { get; init; }
        public DateTime CreatedOn { get; init; }
    }
}