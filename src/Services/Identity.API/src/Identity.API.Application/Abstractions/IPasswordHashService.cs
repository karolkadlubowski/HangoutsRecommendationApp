namespace Identity.API.Application.Abstractions
{
    public interface IPasswordHashService
    {
        byte[] CreatePasswordSalt();
        byte[] HashPassword(string password, byte[] salt);
        bool VerifyPasswordHash(string password, byte[] salt, byte[] hash);
    }
}