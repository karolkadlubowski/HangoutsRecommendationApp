from neo4j import GraphDatabase
from event_type import EventType
from base_kafka import BaseKafka
from neo4j.exceptions import ServiceUnavailable

import json

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
        data_dict = json.loads(message['Data'])
        query = (
            "CREATE (u:User {userId: $userId})"
            "RETURN u"
        )
        result = tx.run(query, userId=data_dict['UserId'])
        try:
            return [{"u": record["u"]}
                    for record in result]
        except ServiceUnavailable as exception:
            print(exception)
            raise
