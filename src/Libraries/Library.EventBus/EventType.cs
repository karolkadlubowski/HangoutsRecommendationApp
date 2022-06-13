namespace Library.EventBus
{
    public enum EventType
    {
        UNDEFINED = 0,
        VENUE_CREATED,
        VENUE_UPDATED,
        VENUE_DELETED,
        USER_EMAIL_CHANGED,
        CATEGORY_ADDED,
        CATEGORY_DELETED,
        ACCOUNT_PROVIDER_ADDED,
        ACCOUNT_PROVIDER_DELETED,
        VENUE_REVIEW_ADDED,
        VENUE_REVIEW_DELETED
    }
}