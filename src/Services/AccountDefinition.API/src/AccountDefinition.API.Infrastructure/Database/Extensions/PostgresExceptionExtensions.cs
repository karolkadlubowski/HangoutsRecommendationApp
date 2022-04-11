using Npgsql;

namespace AccountDefinition.API.Infrastructure.Database.Extensions
{
    public static class PostgresExceptionExtensions
    {
        private const string UniqueConstraintViolationExceptionCode = "23505";

        public static bool IsUniqueConstraintViolationException(this PostgresException e)
            => e.Code == UniqueConstraintViolationExceptionCode;
    }
}