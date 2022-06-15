from event_type import EventType
from base_kafka import BaseKafka

class IdentityKafka(BaseKafka):
    def __init__(self, topic: str):
        super().__init__(topic)

    def consume(self):
        for message in self.consumer:
            message = message.value
            event_type = EventType(message['EventType'])

            print('Event type: ', event_type)
            print(message)

            if EventType.IDENTITY_CREATED == event_type:
                self.callback_identity_created(message)
            elif EventType.IDENTITY_UPDATED == event_type:
                self.callback_identity_updated(message)
            elif EventType.IDENTITY_DELETED == event_type:
                self.callback_identity_deleted(message)

    def callback_identity_created(self, message):
        print('VENUE CREATED')
        print(message)

    def callback_identity_updated(self, message):
        print('VENUE UPDATED')
        print(message)

    def callback_identity_deleted(self, message):
        print('VENUE DELETED')
        print(message)