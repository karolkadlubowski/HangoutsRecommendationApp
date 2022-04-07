using Library.Shared.Constants;

namespace Library.Shared.Exceptions
{
    public class InsufficientPermissionsException : BaseException
    {
        private const string CustomMessage = "You are not allowed to perform this action";

        public InsufficientPermissionsException() : base(CustomMessage, ErrorCodes.InsufficientPermissionsException)
        {
        }

        public InsufficientPermissionsException(string message = CustomMessage,
            string errorCode = ErrorCodes.InsufficientPermissionsException) : base(message, errorCode)
        {
        }
    }
}