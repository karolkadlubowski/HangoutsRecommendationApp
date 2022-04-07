namespace Library.Shared.Constants
{
    public static class LoggingConstants
    {
        public const string NlogFileName = "nlog.config";

        public const string Scope = nameof(Scope);

        public static string GetScopeValue(params string[] parameters) => $"{string.Join(" | ", parameters)} |";
    }
}