db.createUser(
    {
        user: "admin",
        pwd: "admin1234",
        roles: [
            {
                role: "readWrite",
                db: "VENUELIST_DB"
            }
        ]
    }
);

db = new Mongo().getDB("VENUELIST_DB");

db.createCollection("Venues");