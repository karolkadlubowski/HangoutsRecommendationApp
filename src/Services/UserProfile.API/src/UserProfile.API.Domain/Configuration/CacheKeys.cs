namespace UserProfile.API.Domain.Configuration
{
    public static class CacheKeys
    {
        public static string UserKey(long userId) => $"USER:{userId}";
    }
}