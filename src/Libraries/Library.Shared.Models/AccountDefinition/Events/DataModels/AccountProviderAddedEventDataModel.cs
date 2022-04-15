namespace Library.Shared.Models.AccountDefinition.Events.DataModels
{
    public record AccountProviderAddedEventDataModel
    {
        public long AccountProviderId { get; init; }
        public string Provider { get; init; }
    }
}