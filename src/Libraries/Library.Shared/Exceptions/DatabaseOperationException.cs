using Library.Shared.Constants;

namespace Library.Shared.Exceptions
{
    public class DatabaseOperationException : BaseException
    {
        private const string CustomMessage = "Some database operation failed";

        public DatabaseOperationException(string message = CustomMessage,
            string errorCode = ErrorCodes.DatabaseOperationFailed) : base(
            message, errorCode)
        {
        }
    }
}