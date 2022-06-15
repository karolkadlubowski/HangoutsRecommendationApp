from flask import Flask, request, jsonify
from venue_kafka import VenueKafka
from identity_kafka import IdentityKafka

app = Flask(__name__)

@app.route('/venue/algorithm/venues', methods=['GET'])
def get_venues():
    print('Get Venues')

    user_id = request.args['userId']
    
    res = jsonify(data={'userId': user_id}, success=True)
    print(res)

    return res

if __name__ == '__main__':
    venue_kafka = VenueKafka(topic='venue')
    identity_kafka = IdentityKafka(topic='identity')

    venue_kafka.run()
    identity_kafka.run()

    app.run(debug=True)