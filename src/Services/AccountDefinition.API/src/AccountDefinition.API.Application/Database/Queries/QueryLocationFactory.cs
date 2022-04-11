using System.Reflection;

namespace AccountDefinition.API.Application.Database.Queries
{
    public static class QueryLocationFactory
    {
        private const string QueriesAssemblyName = "AccountDefinition.API.Infrastructure";
        private const string SqlExtension = ".sql";

        private readonly static string _baseLocation = $"{QueriesAssemblyName}.Database.Queries";

        public static Assembly QueriesAssembly => Assembly.Load(QueriesAssemblyName);

        public static string PrepareQueryLocation(string queryName)
            => $"{_baseLocation}.{queryName}{SqlExtension}";
    }
}