namespace Identity.API.Domain.Validation
{
    public static class ValidationRules
    {
        public const int MinPasswordLength = 4;
        public const int MaxPasswordLength = 64;

        public const int MaxEmailAddress = 255;
    }
}