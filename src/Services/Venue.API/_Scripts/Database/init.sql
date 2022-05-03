CREATE SCHEMA IF NOT EXISTS "Venue";

CREATE TABLE IF NOT EXISTS "Venue"."Venues"
(
    "VenueId"       bigserial PRIMARY KEY,
    "Name"          varchar(200)                                                   NOT NULL,
    "Description"   varchar(2000),
    "LocationId"    bigint UNIQUE,                                                  
    "CategoryId"    varchar(24)                                                    NOT NULL,
    "CreatorId"     bigint,
    "Status"        int DEFAULT 0,
    "PersistState"  int DEFAULT 0,
    "CreatedOn"     timestamp without time zone DEFAULT (now() at time zone 'utc') NOT NULL,
    "ModifiedOn"    timestamp without time zone
);

--INDEXES--
CREATE INDEX ON "Venue"."Venues" ("LocationId");
CREATE INDEX ON "Venue"."Venues" ("CategoryId");