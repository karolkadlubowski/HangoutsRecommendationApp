namespace Library.Shared.Models.AccountDefinition.Events.DataModels
{
    public record AccountProviderDeletedEventDataModel
    {
        public long AccountProviderId { get; init; }
    }
}