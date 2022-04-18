db.createUser(
    {
        user: "admin",
        pwd: "admin1234",
        roles: [
            {
                role: "readWrite",
                db: "CATEGORY_DB"
            }
        ]
    }
);

db = new Mongo().getDB("CATEGORY_DB");

db.createCollection("Categories");

db.Categories.insert(
    { Name: "RESTAURANT" },
    { Name: "FAST-FOOD" },
    { Name: "MUSEUM" },
    { Name: "SPORT" },
    { Name: "RELIGIOUS" },
    { Name: "PARK" },
    { Name: "MOUNTAIN" },
    { Name: "SWIMMING" }
);