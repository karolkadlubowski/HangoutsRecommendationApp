db.createUser(
    {
        user: "admin",
        pwd: "admin1234",
        roles: [
            {
                role: "readWrite",
                db: "FILESTORAGE_DB"
            }
        ]
    }
);

db = new Mongo().getDB("FILESTORAGE_DB");

db.createCollection("Folders");

db.Folders.createIndex({ "Key": 1 });