from flask import Flask, request, jsonify

app = Flask(__name__)

@app.route('/venue/algorithm/venues', methods=['GET'])
def get_venues():
    print('Get Venues')

    user_id = request.args['UserId']
    
    res = jsonify(data={'UserId': user_id}, success=True)
    print(res)

    return res

if __name__ == '__main__':
    app.run(debug=True)