namespace Library.Shared.Models.Identity.Events.DataModels
{
    public record UserCreatedEventDataModel
    {
        public long UserId { get; init; }
        public string EmailAddress { get; init; }
    }
}