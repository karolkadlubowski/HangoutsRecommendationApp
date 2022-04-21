namespace Library.Shared.Models.Category.Events.DataModels
{
    public record CategoryDeletedEventDataModel
    {
        public string CategoryId { get; init; }
    }
}