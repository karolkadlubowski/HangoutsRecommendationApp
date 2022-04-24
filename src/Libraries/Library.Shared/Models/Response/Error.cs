using System.Collections.Generic;

namespace Library.Shared.Models.Response
{
    public record Error
    (
        string ErrorCode,
        string Message,
        IDictionary<string, IEnumerable<string>> Validations = null
    );
}