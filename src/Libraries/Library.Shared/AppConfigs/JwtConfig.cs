namespace Library.Shared.AppConfigs
{
    public record JwtConfig
    {
        public const string JwtSecretKey = "JwtConfig:Secret";

        public string Secret { get; init; }
        public int TokenExpirationInMinutes { get; init; }
    }
}