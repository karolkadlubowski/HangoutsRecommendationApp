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

            if EventType.IDENTITY_CREATED == event_type:
                self.__callback_identity_created(message)
            elif EventType.IDENTITY_UPDATED == event_type:
                self.__callback_identity_updated(message)
            elif EventType.IDENTITY_DELETED == event_type:
                self.__callback_identity_deleted(message)

    def __callback_identity_created(self, message):
        print('VENUE CREATED')

        with self.driver.session() as session:
            result = session.write_transaction(self.__create_identity, message)
            print(result)

    def __callback_identity_updated(self, message):
        print('VENUE UPDATED')

        with self.driver.session() as session:
            result = session.write_transaction(self.__update_identity, message)
            print(result)

    def __callback_identity_deleted(self, message):
        print('VENUE DELETED')

        with self.driver.session() as session:
            result = session.write_transaction(self.__delete_identity, message)
            print(result)

    @staticmethod
    def __create_identity(tx, messsage):
        pass

    @staticmethod
    def __update_identity(tx, messsage):
        pass

    @staticmethod
    def __delete_identity(tx, messsage):
        pass
