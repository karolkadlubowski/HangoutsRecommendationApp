CREATE SCHEMA IF NOT EXISTS "AccountDefinition";

CREATE TABLE IF NOT EXISTS "AccountDefinition"."AccountProviders"
(
    "AccountProviderId" bigserial PRIMARY KEY,
    "Provider"          int UNIQUE,
    "CreatedOn"         timestamp without time zone DEFAULT (now() at time zone 'utc') NOT NULL,
    "ModifiedOn"        timestamp without time zone
);

CREATE TABLE IF NOT EXISTS "AccountDefinition"."AccountTypes"
(
    "AccountTypeId" bigserial PRIMARY KEY,
    "Type"          int UNIQUE,
    "CreatedOn"     timestamp without time zone DEFAULT (now() at time zone 'utc') NOT NULL,
    "ModifiedOn"    timestamp without time zone
);

--DATA--
DELETE
FROM "AccountDefinition"."AccountProviders";
INSERT INTO "AccountDefinition"."AccountProviders" ("AccountProviderId", "Provider")
VALUES (1, 0),
       (2, 1),
       (3, 2);
SELECT setval(pg_get_serial_sequence('"AccountDefinition"."AccountProviders"', 'AccountProviderId'),
              (SELECT max("AccountProviderId") FROM "AccountDefinition"."AccountProviders"));

DELETE
FROM "AccountDefinition"."AccountTypes";
INSERT INTO "AccountDefinition"."AccountTypes" ("AccountTypeId", "Type")
VALUES (1, 0),
       (2, 1),
       (3, 2);
SELECT setval(pg_get_serial_sequence('"AccountDefinition"."AccountTypes"', 'AccountTypeId'),
              (SELECT max("AccountTypeId") FROM "AccountDefinition"."AccountTypes"));