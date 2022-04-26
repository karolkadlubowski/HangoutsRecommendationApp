using Library.Shared.Models;
using UserProfile.API.Domain.ValueObjects;

namespace UserProfile.API.Domain.Entities
{
    public class UserProfile : RootEntity
    {
        public long UserId { get; protected set; }
        public string EmailAddress { get; protected set; }

        public static UserProfile Create(string emailAdress)
            => new UserProfile {EmailAddress = new EmailAddress(emailAdress)};
    }
}