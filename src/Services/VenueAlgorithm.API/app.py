import json
import jwt

from flask import Flask, request, jsonify
from venue_kafka import VenueKafka
from identity_kafka import IdentityKafka
from neo4j import GraphDatabase
from neo4j.exceptions import ServiceUnavailable

app = Flask(__name__)

app.config['SECRET_KEY'] = 'th1s1sjust43x4mpl3'

driver = GraphDatabase.driver('bolt://localhost:7687', auth=('neo4j', 'admin'))

def decode_auth_token(auth_token):
    payload = jwt.decode(auth_token, app.config.get('SECRET_KEY'), algorithms=['HS512'])

    return payload['nameid']


@app.route('/venue/algorithm/venues', methods=['GET'])
def route_get_venues():
    print('Get Venues')
    
    auth_token = request.headers.get('Authorization')
    auth_token = auth_token.split(' ')[-1]

    args = {
        "userId": decode_auth_token(auth_token)
    }

    with driver.session() as session:
        result = session.write_transaction(get_venues, args)
        print('RESULT: ', result)
    
        res = jsonify(data={'venueIds': result}, success=True)
        print('RES: ', res)

        return res

@app.route('/venue/algorithm/like', methods=['PUT'])
def route_like_venue():
    print('Update relation')

    auth_token = request.headers.get('Authorization')
    auth_token = auth_token.split(' ')[-1]

    args = {
        "userId": decode_auth_token(auth_token),
        'venueId': request.args['venueId']
    }

    with driver.session() as session:
        result = session.write_transaction(update_relation, args)
        print('RESULT: ', result)

        res = jsonify(success=True)
        print(res)

        return res

def get_venues(tx, message):
    query = (
            "MATCH (u1:User {userId: $userId})-[:liked|:saved]->(u1Venue:Venue)<-[:liked|:saved]-(similarUser1:User)-[:liked|:saved]->(similarUserVenue1:Venue) "
            "WHERE similarUser1 <> u1 and similarUserVenue1 <> u1Venue "
            "RETURN DISTINCT similarUserVenue1"
        )

    result = tx.run(query, userId=int(message['userId']))
    try:
        result_array = [record['similarUserVenue1']['venueId']
                for record in result]
        print('result_array: ', result_array)
        return result_array
    except ServiceUnavailable as exception:
        print(exception)
        raise
    pass

def update_relation(tx, message):
    query = (
        "MATCH (u1:User), (v1:Venue) "
        "WHERE u1.userId = $userId and v1.venueId = $venueId "
        "CREATE (u1)-[r:liked]->(v1) "
        "RETURN r"
    )
    result = tx.run(query, userId=int(message['userId']), venueId=int(message['venueId']))
    try:
        return [{"r": record["r"]}
                for record in result]
    except ServiceUnavailable as exception:
        print(exception)
        raise
    pass


if __name__ == '__main__':
    venue_kafka = VenueKafka('venue', driver)
    identity_kafka = IdentityKafka('identity', driver)

    venue_kafka.run()
    identity_kafka.run()

    app.run(debug=True)
