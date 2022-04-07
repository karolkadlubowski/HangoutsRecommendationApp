using Library.Shared.Constants;

namespace Library.Shared.Exceptions
{
    public class ServiceUnavailableException : BaseException
    {
        public ServiceUnavailableException(string message, string errorCode = ErrorCodes.ServiceUnavailableError) : base(message, errorCode)
        {
        }
    }
}