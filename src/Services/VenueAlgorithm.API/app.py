from flask import Flask, request, jsonify
from venue_kafka import VenueKafka
from identity_kafka import IdentityKafka
from neo4j import GraphDatabase
from neo4j.exceptions import ServiceUnavailable
import json
import jwt

app = Flask(__name__)

app.config['SECRET_KEY'] = 'th1s1sjust43x4mpl3'

driver = GraphDatabase.driver('bolt://localhost:7687', auth=('neo4j', 'admin'))

def decode_auth_token(auth_token):
    """
    Decodes the auth token
    :param auth_token:
    :return: integer|string
    """

    payload = jwt.decode(auth_token, app.config.get('SECRET_KEY'), algorithms=['HS512'])

    return payload['sub']


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
        print(result)
    
    res = jsonify(data={'venueIds': [1, 2, 3, 4]}, success=True)
    print(res)

    return res

@app.route('/venue/algorithm/relation', methods=['PUT'])
def route_update_relation():
    print('Update relation')

    auth_token = request.headers.get('Authorization')
    auth_token = auth_token.split(' ')[-1]

    args = {
        "userId": decode_auth_token(auth_token),
        'venueId': request.args['venueId'],
        'relationType': request.args['relationType']
    }

    with driver.session() as session:
        result = session.write_transaction(get_venues, args)
        print(result)

        res = jsonify(success=True)
        print(res)

        return res

def get_venues(tx, message):
    # data_dict = json.loads(message['Data'])
    # query = (
    #     "MATCH (u1:User {userId: $userId})-[:liked]->(u1Venue:Venue)<-[:liked|:saved]-(similarUser1:User)-[:liked|:saved]->(similarUserVenue1:Venue) where similarUser1 <> u1 and similarUserVenue1 <> u1Venue LIMIT 5"
    #     "MATCH (u2:User {userId: $userId})-[:saved]->(u2Venue:Venue)<-[:liked|:saved]-(similarUser2:User)-[:liked|:saved]->(similarUserVenue2:Venue) where similarUser2 <> u2 and similarUserVenue2 <> u2Venue LIMIT 5"
    #     "RETURN similarUserVenue1, similarUserVenue2"
    # )
    # result = tx.run(query, userId=data_dict['UserId'])
    # try:
    #     return [{"similarUserVenue1": record["similarUserVenue1"]["venueId"], "similarUserVenue2": record["similarUserVenue2"]["venueId"]}
    #             for record in result]
    # except ServiceUnavailable as exception:
    #     print(exception)
    #     raise
    pass

def update_relation(tx, message):
    # data_dict = json.loads(message['Data'])
    # query = (
    #     "MATCH (u1:User {userId: $userId}), (v1:Venue {venueId: $venueId})"
    #     "CREATE (u1)-[r:liked]->(v1)"
    #     "RETURN u1"
    # )
    # result = tx.run(query, userId=data_dict['UserId'], venueId=data_dict['VenueId'])
    # try:
    #     return [{"r": record["u1"]}
    #             for record in result]
    # except ServiceUnavailable as exception:
    #     print(exception)
    #     raise
    pass


if __name__ == '__main__':
    venue_kafka = VenueKafka('venue', driver)
    identity_kafka = IdentityKafka('identity', driver)

    venue_kafka.run()
    identity_kafka.run()

    app.run(debug=True)
