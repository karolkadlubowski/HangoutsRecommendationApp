using System;

namespace AccountDefinition.API.Application.Database.PersistenceModels
{
    public record AccountProviderPersistenceModel
    {
        public long AccountProviderId { get; init; }
        public string Provider { get; init; }
        public DateTime CreatedOn { get; init; }
        public DateTime? ModifiedOn { get; init; }
    }
}