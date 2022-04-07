using System.Collections.Generic;
using System.Net;

namespace Library.Shared.Models.Response
{
    public record Error
    (
        string ErrorCode,
        string Message,
        HttpStatusCode StatusCode = HttpStatusCode.InternalServerError,
        IDictionary<string, IEnumerable<string>> Validations = null
    );
}