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

db.Categories.insertMany([
    { Name: "RESTAURANT", CreatedOn: new ISODate() },
    { Name: "FAST-FOOD", CreatedOn: new ISODate() },
    { Name: "MUSEUM", CreatedOn: new ISODate() },
    { Name: "SPORT", CreatedOn: new ISODate() },
    { Name: "RELIGIOUS", CreatedOn: new ISODate() },
    { Name: "PARK", CreatedOn: new ISODate() },
    { Name: "MOUNTAIN", CreatedOn: new ISODate() },
    { Name: "SWIMMING", CreatedOn: new ISODate() }
]);