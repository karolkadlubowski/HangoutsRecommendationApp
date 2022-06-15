import json

from neo4j import GraphDatabase
from kafka import KafkaConsumer
from threading import Thread

class BaseKafka():
    def __init__(self, topic: str, driver: GraphDatabase):
        self.driver = driver
        self.consumer = KafkaConsumer(topic, bootstrap_servers=['localhost:29092'],
                                            api_version=(0,10),
                                            auto_offset_reset='earliest',
                                            enable_auto_commit=True,
                                            group_id='my-group',
                                            value_deserializer=lambda x: json.loads(x.decode('utf-8')))
        
        self.thread = Thread(target=self.consume)

    def run(self):
        print('Start consume')
        self.thread.start()
        print('Threads initialized')

    def consume(self):
        pass