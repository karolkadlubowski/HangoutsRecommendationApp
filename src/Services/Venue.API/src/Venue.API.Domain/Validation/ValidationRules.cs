namespace Venue.API.Domain.Validation
{
    public static class ValidationRules
    {
        public const int MaxVenueNameLength = 200;
        public const int MaxVenueDescriptionLength = 2000;
        public const int CategoryIdLength = 24;
        public const int MaxPhotosCount = 6;
    }
}