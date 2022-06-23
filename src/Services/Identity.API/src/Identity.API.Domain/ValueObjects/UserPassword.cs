using Library.Shared.Exceptions;
using Library.Shared.Models;

namespace Identity.API.Domain.ValueObjects
{
    public record UserPassword : ValueObject<string>
    {
        public UserPassword(string password)
            => Value = string.IsNullOrWhiteSpace(password)
                ? throw new ValidationException($"{nameof(password)} cannot be null or empty")
                : password.Trim();
    }
}