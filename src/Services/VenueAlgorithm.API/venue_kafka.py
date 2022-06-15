from neo4j import GraphDatabase
from event_type import EventType
from base_kafka import BaseKafka

class VenueKafka(BaseKafka):
    def __init__(self, topic: str, driver: GraphDatabase):
        super().__init__(topic, driver)

    def consume(self):
        for message in self.consumer:
            message = message.value
            event_type = EventType(message['EventType'])

            print('Event type: ', event_type)

            if EventType.VENUE_CREATED == event_type:
                self.__callback_venue_created(message)
            elif EventType.VENUE_UPDATED == event_type:
                self.__callback_venue_updated(message)
            elif EventType.VENUE_DELETED == event_type:
                self.__callback_venue_deleted(message)

    def __callback_venue_created(self, message):
        print('VENUE CREATED')

        with self.driver.session() as session:
            result = session.write_transaction(self.__create_venue, message)
            print(result)

    def __callback_venue_updated(self, message):
        print('VENUE UPDATED')

        with self.driver.session() as session:
            result = session.write_transaction(self.__update_venue, message)
            print(result)

    def __callback_venue_deleted(self, message):
        print('VENUE DELETED')

        with self.driver.session() as session:
            result = session.write_transaction(self.__delete_venue, message)
            print(result)

    @staticmethod
    def __create_venue(tx, messsage):
        pass

    @staticmethod
    def __update_venue(tx, messsage):
        pass

    @staticmethod
    def __delete_venue(tx, messsage):
        pass