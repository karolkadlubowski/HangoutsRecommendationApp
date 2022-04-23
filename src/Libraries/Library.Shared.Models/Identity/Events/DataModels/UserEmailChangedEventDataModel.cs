namespace Library.Shared.Models.Identity.Events.DataModels
{
    public record UserEmailChangedEventDataModel
    {
        public long UserId { get; init; }
        public string CurrentEmailAddress { get; init; }
    }
}