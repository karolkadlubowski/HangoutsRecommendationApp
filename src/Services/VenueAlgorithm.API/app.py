import json

from flask import Flask, request, jsonify
from kafka import KafkaConsumer

app = Flask(__name__)

@app.route('/venue/algorithm/venues', methods=['GET'])
def get_venues():
    print('Get Venues')

    user_id = request.args['UserId']
    
    res = jsonify(data={'UserId': user_id}, success=True)
    print(res)

    return res

class VenueKafka:
    def __init__(self):
        self.venue_consumer = KafkaConsumer('venue', bootstrap_servers=['localhost:29092'],
                                            api_version=(0,10),
                                            auto_offset_reset='earliest',
                                            enable_auto_commit=True,
                                            group_id='my-group',
                                            value_deserializer=lambda x: json.loads(x.decode('utf-8')))

    def consume(self):
        print('Start consmue')

        for message in self.venue_consumer:
            message = message.value
            print(message)

if __name__ == '__main__':
    # app.run(debug=True)
    venue_kafka = VenueKafka()
    venue_kafka.consume()