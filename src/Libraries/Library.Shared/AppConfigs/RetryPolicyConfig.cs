namespace Library.Shared.AppConfigs
{
    public record RetryPolicyConfig
    {
        public int RetryAttemptDelayInSeconds { get; init; }
    }
}