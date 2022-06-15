from flask import Flask, request, jsonify
from venue_kafka import VenueKafka
from identity_kafka import IdentityKafka
from neo4j import GraphDatabase

app = Flask(__name__)

@app.route('/venue/algorithm/venues', methods=['GET'])
def get_venues():
    print('Get Venues')

    user_id = request.args['userId']
    
    res = jsonify(data={'userId': user_id}, success=True)
    print(res)

    return res

@app.route('/venue/algorithm/relation', methods=['PUT'])
def update_relation():
    user_id = request.args['userId']
    venue_id = request.args['venueId']

    res = jsonify(data={'userId': user_id, 'venueId': venue_id}, success=True)

    return res

if __name__ == '__main__':
    driver = GraphDatabase.driver('bolt://localhost:7687', auth=('neo4j', 'neo4j'))
    venue_kafka = VenueKafka('venue', driver)
    identity_kafka = IdentityKafka('identity', driver)

    venue_kafka.run()
    identity_kafka.run()

    app.run(debug=True)