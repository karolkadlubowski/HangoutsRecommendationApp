namespace Library.Shared.Models.Category.Events.DataModels
{
    public record CategoryAddedEventDataModel
    {
        public string CategoryId { get; init; }
        public string Name { get; init; }
    }
}