using System.Data.Common;

namespace Library.Database.Extensions
{
    public static class DbExceptionExtensions
    {
        private const string UniqueConstraintViolationExceptionCode = "23505";

        public static bool IsUniqueConstraintViolationException(this DbException e)
            => e.SqlState == UniqueConstraintViolationExceptionCode;
    }
}