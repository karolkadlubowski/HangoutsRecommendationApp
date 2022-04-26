using System.Text.RegularExpressions;
using FluentValidation;
using Library.Shared.Models;
using UserProfile.API.Domain.Validation;

namespace UserProfile.API.Domain.ValueObjects
{
    public record EmailAddress : ValueObject<string>
    {
        public EmailAddress(string emailAddress)
        {
            var regex = new Regex(ValidationRules.emailAdressRegex);
            if (regex.Match(emailAddress).Success)
                Value = emailAddress;
            else
            {
                throw new ValidationException($"{emailAddress} is not correct email");
            }
        }
    }
}