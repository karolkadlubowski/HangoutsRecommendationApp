using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Library.Shared.Constants;

namespace Library.Shared.Exceptions
{
    public class ValidationException : BaseException
    {
        public IDictionary<string, IEnumerable<string>> Errors { get; }

        public const string CustomMessage = "One or more validation failures have occurred";

        public ValidationException(string message = CustomMessage, string errorCode = ErrorCodes.ValidationFailed) :
            base(message, errorCode)
        {
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(group => group.Key, group => group.AsEnumerable());
        }
    }
}