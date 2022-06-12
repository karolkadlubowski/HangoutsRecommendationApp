using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using Identity.API.Domain.Validation;
using Library.Shared.Exceptions;
using Library.Shared.Models;
using ValidationException = Library.Shared.Exceptions.ValidationException;

namespace Identity.API.Domain.ValueObjects
{
    public record UserEmail : ValueObject<string>
    {
        public UserEmail(string email)
            => Value = email != null && new EmailAddressAttribute().IsValid(email)
                ? email.Length < ValidationRules.MaxEmailAddress
                    ? email
                    : throw new ValidationException($"{nameof(email)} cannot exceed {ValidationRules.MaxEmailAddress} characters")
                : throw new ValidationException($"{nameof(email)} is not valid");
    }
}