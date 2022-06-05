db.createUser(
    {
        user: "admin",
        pwd: "admin1234",
        roles: [
            {
                role: "readWrite",
                db: "VENUEREVIEW_DB"
            }
        ]
    }
);

db = new Mongo().getDB("VENUEREVIEW_DB");

db.createCollection("VenueReviews");