using System;
using System.Collections.Generic;
using System.Net;
using Library.Shared.Exceptions;

namespace Library.Shared.Dictionaries
{
    public static class ExceptionDictionary
    {
        private static readonly IReadOnlyDictionary<Type, HttpStatusCode> _exceptionDictionary =
            new Dictionary<Type, HttpStatusCode>
            {
                [typeof(BaseException)] = HttpStatusCode.InternalServerError,
                [typeof(ServerException)] = HttpStatusCode.InternalServerError,
                [typeof(DatabaseOperationException)] = HttpStatusCode.InternalServerError,
                [typeof(ServiceUnavailableException)] = HttpStatusCode.ServiceUnavailable,
                [typeof(EntityNotFoundException)] = HttpStatusCode.NotFound,
                [typeof(InsufficientPermissionsException)] = HttpStatusCode.Forbidden,
                [typeof(ValidationException)] = HttpStatusCode.UnprocessableEntity,
                [typeof(InvalidCredentialsException)] = HttpStatusCode.Unauthorized,
                [typeof(DuplicateExistsException)] = HttpStatusCode.Conflict
            };

        public static HttpStatusCode GetStatusCode(Exception exception)
            => _exceptionDictionary.TryGetValue(exception.GetType(), out var value)
                ? value
                : HttpStatusCode.InternalServerError;
    }
}