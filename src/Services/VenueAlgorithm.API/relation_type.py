from enum import Enum, auto

class RelationType(Enum):
    NOTHING = 0
    LIKED = auto()
    SAVED = auto()