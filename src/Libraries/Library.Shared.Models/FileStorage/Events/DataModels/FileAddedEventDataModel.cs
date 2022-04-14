namespace Library.Shared.Models.FileStorage.Events.DataModels
{
    public record FileAddedEventDataModel
    {
        public string FileKey { get; init; }
        public string FileUrl { get; init; }
    }
}