from neo4j import GraphDatabase
from neo4j.exceptions import ServiceUnavailable
from event_type import EventType
from base_kafka import BaseKafka

import json

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
            elif EventType.VENUE_ADDED_TO_FAVORITES == event_type:
                self.__callback_venue_added_to_favorites(message)
            elif EventType.VENUE_DELETED_FROM_FAVORITES == event_type:
                self.__callback_venue_deleted_from_favorites(message)

    def __callback_venue_created(self, message):
        print('VENUE CREATED')

        with self.driver.session() as session:
            result = session.write_transaction(self.__create_venue, message)
            print('result: ', result)

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

    def __callback_venue_added_to_favorites(self, message):
        print('VENUE ADDED TO FAVORITES')

        with self.driver.session() as session:
            result = session.write_transaction(self.__add_venue_to_favorites, message)
            print(result)

    def __callback_venue_deleted_from_favorites(self, message):
        print('VENUE ADDED TO FAVORITES')

        with self.driver.session() as session:
            result = session.write_transaction(self.__delete_venue_from_favorites, message)
            print(result)

    @staticmethod
    def __create_venue(tx, message):
        data_dict = json.loads(message['Data'])
        query = (
            "CREATE (v:Venue {venueId: $venueId, categoryId: $categoryId, style: $style, occupancy: $occupancy})"
            "RETURN v"
        )
        result = tx.run(query, venueId=data_dict['VenueId'], categoryId=data_dict['CategoryId'], style=data_dict['Style'], occupancy=data_dict['Occupancy'])
        try:
            return [{"v": record["v"]}
                    for record in result]
        except ServiceUnavailable as exception:
            print(exception)
            raise

    @staticmethod
    def __update_venue(tx, message):
        data_dict = json.loads(message['Data'])
        query = (
            "MATCH (v:Venue {venueId: $venueId})"
            "SET v.categoryId = $categoryId, v.style = $style, v.occupancy = $occupancy RETURN v"
        )
        result = tx.run(query, venueId=data_dict['VenueId'], categoryId=data_dict['CategoryId'],
                        style=data_dict['Style'], occupancy=data_dict['Occupancy'])
        try:
            return [{"v": record["v"]}
                    for record in result]
        except ServiceUnavailable as exception:
            print(exception)
            raise

    @staticmethod
    def __delete_venue(tx, message):
        data_dict = json.loads(message['Data'])
        query = (
            "MATCH (v:Venue {venueId: $venueId})" 
            "DELETE v"
        )
        result = tx.run(query, venueId=data_dict['VenueId'])
        try:
            return [{"v": record["v"]}
                    for record in result]
        except ServiceUnavailable as exception:
            print(exception)
            raise

    @staticmethod
    def __add_venue_to_favorites(tx, message):
        pass

    @staticmethod
    def __delete_venue_from_favorites(tx, message):
        pass