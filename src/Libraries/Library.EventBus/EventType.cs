namespace Library.EventBus
{
    public enum EventType
    {
        UNDEFINED = 0,
        VENUE_CREATED_WITHOUT_LOCATION,
        VENUE_LOCATION_DELETED,
        USER_EMAIL_CHANGED,
        CATEGORY_ADDED,
        CATEGORY_DELETED,
        ACCOUNT_PROVIDER_ADDED,
        ACCOUNT_PROVIDER_DELETED
    }
}