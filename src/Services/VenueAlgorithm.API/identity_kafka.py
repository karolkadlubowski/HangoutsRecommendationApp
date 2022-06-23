from neo4j import GraphDatabase
from event_type import EventType
from base_kafka import BaseKafka

class IdentityKafka(BaseKafka):
    def __init__(self, topic: str, driver: GraphDatabase):
        super().__init__(topic, driver)

    def consume(self):
        for message in self.consumer:
            message = message.value
            event_type = EventType(message['EventType'])

            print('Event type: ', event_type)
            print(message)

            if EventType.USER_CREATED == event_type:
                self.__callback_identity_created(message)

    def __callback_identity_created(self, message):
        print('IDENTITY CREATED')

        with self.driver.session() as session:
            result = session.write_transaction(self.__create_identity, message)
            print(result)

    @staticmethod
    def __create_identity(tx, message):
        print(message)
        pass
