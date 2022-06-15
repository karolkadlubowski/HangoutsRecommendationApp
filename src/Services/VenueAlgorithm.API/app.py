from flask import Flask, request, jsonify
from venue_kafka import VenueKafka
from identity_kafka import IdentityKafka
from neo4j import GraphDatabase

app = Flask(__name__)

driver = GraphDatabase.driver('bolt://localhost:7687', auth=('neo4j', 'admin'))

@app.route('/venue/algorithm/venues', methods=['GET'])
def route_get_venues():
    print('Get Venues')

    args = {
        'userId': request.args['userId']
    }

    with driver.session() as session:
        result = session.write_transaction(get_venues, args)
        print(result)
    
        res = jsonify(data=args, success=True)
        print(res)

        return res

@app.route('/venue/algorithm/relation', methods=['PUT'])
def route_update_relation():
    print('Update relation')

    args = {
        'userId': request.args['userId'],
        'venueId': request.args['venueId'],
        'relationType': request.args['relationType']
    }

    with driver.session() as session:
        result = session.write_transaction(get_venues, args)
        print(result)

        res = jsonify(data=args, success=True)
        print(res)

        return res

def get_venues(tx, message):
    pass

def update_relation(tx, message):
    pass


if __name__ == '__main__':
    venue_kafka = VenueKafka('venue', driver)
    identity_kafka = IdentityKafka('identity', driver)

    venue_kafka.run()
    identity_kafka.run()

    app.run(debug=True)