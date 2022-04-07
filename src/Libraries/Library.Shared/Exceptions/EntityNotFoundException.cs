using Library.Shared.Constants;

namespace Library.Shared.Exceptions
{
    public class EntityNotFoundException : BaseException
    {
        public EntityNotFoundException(string message, string errorCode = ErrorCodes.EntityNotFound) : base(message,
            errorCode)
        {
        }
    }
}