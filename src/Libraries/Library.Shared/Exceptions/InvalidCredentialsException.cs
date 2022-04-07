using Library.Shared.Constants;

namespace Library.Shared.Exceptions
{
    public class InvalidCredentialsException : BaseException
    {
        public InvalidCredentialsException(string message, string errorCode = ErrorCodes.InvalidCredentials) : base(message,
            errorCode)
        {
        }
    }
}