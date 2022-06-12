using Identity.API.Domain.ValueObjects;
using Library.Shared.Models;

namespace Identity.API.Domain.Entities
{
    public class User : RootEntity
    {
        public long UserId { get; protected set; }
        public string Email { get; protected set; }
        public byte[] PasswordHash { get; protected set; }
        public byte[] PasswordSalt { get; protected set; }

        public User(string email, byte[] passwordSalt, byte[] passwordHash)
        {
            Email = new UserEmail(email);
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
        }

        public void SetPassword(byte[] passwordSalt, byte[] passwordHash)
        {
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
        }
    }
}