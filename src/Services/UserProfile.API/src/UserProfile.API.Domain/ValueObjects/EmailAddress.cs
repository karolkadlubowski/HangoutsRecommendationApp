using System.Text.RegularExpressions;
using Library.Shared.Exceptions;
using Library.Shared.Models;
using UserProfile.API.Domain.Validation;

namespace UserProfile.API.Domain.ValueObjects
{
    public record EmailAddress : ValueObject<string>
    {
        public EmailAddress(string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
                throw new ValidationException($"{nameof(emailAddress)} cannot be null or empty");

            Value = Regex.Match(emailAddress, ValidationRules.EmailAdressRegex).Success
                ? emailAddress
                : throw new ValidationException($"{emailAddress} has invalid email address format");
        }
    }
}