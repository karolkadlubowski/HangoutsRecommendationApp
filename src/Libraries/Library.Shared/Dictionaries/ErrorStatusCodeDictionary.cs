using System.Collections.Generic;
using System.Net;
using Library.Shared.Constants;

namespace Library.Shared.Dictionaries
{
    public static class ErrorStatusCodeDictionary
    {
        private static readonly IReadOnlyDictionary<string, HttpStatusCode> _errorStatusCodeDictionary =
            new Dictionary<string, HttpStatusCode>
            {
                [ErrorCodes.ServerError] = HttpStatusCode.InternalServerError,
                [ErrorCodes.DatabaseOperationFailed] = HttpStatusCode.InternalServerError,
                [ErrorCodes.ServiceUnavailableError] = HttpStatusCode.ServiceUnavailable,
                [ErrorCodes.EntityNotFound] = HttpStatusCode.NotFound,
                [ErrorCodes.InsufficientPermissionsException] = HttpStatusCode.Forbidden,
                [ErrorCodes.ValidationFailed] = HttpStatusCode.UnprocessableEntity,
                [ErrorCodes.InvalidCredentials] = HttpStatusCode.Unauthorized,
                [ErrorCodes.DuplicateExists] = HttpStatusCode.Conflict
            };

        public static HttpStatusCode GetStatusCode(string errorCode)
            => _errorStatusCodeDictionary.TryGetValue(errorCode, out var value)
                ? value
                : HttpStatusCode.InternalServerError;
    }
}