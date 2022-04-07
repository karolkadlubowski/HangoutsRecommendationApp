using Library.Shared.Constants;

namespace Library.Shared.Exceptions
{
    public class ServerException : BaseException
    {
        public ServerException(string message, string errorCode = ErrorCodes.ServerError) : base(message, errorCode)
        {
        }
    }
}