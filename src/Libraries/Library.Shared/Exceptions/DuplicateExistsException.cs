using Library.Shared.Constants;

namespace Library.Shared.Exceptions
{
    public class DuplicateExistsException : BaseException
    {
        private const string CustomMessage = "Duplicate already exists";

        public DuplicateExistsException(string message = CustomMessage, string errorCode = ErrorCodes.DuplicateExists) :
            base(message, errorCode)
        {
        }
    }
}