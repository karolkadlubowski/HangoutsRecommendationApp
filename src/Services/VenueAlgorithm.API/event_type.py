from enum import Enum, auto

class EventType(Enum):
    UNDEFINED = 0
    VENUE_CREATED = auto()
    VENUE_UPDATED = auto()
    VENUE_DELETED = auto()
    USER_EMAIL_CHANGED = auto()
    CATEGORY_ADDED = auto()
    CATEGORY_DELETED = auto()
    ACCOUNT_PROVIDER_ADDED = auto()
    ACCOUNT_PROVIDER_DELETED = auto()
    VENUE_REVIEW_ADDED = auto()
    VENUE_REVIEW_DELETED = auto()