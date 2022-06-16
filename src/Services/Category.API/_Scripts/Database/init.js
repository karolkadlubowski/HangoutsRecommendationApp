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
    { Name: "FOOD", CreatedOn: new ISODate() },
    { Name: "CULTURAL", CreatedOn: new ISODate() },
    { Name: "SPORT", CreatedOn: new ISODate() },
    { Name: "NATURE", CreatedOn: new ISODate() }
]);