CREATE SCHEMA IF NOT EXISTS "Venue";

CREATE TABLE IF NOT EXISTS "Venue"."Venues"
(
    "VenueId"       bigserial PRIMARY KEY,
    "Name"          varchar(200)                                                   NOT NULL,
    "Description"   varchar(2000),
    "LocationId"    bigint                                                         NOT NULL,                                                  
    "CategoryId"    varchar(24)                                                    NOT NULL,
    "CreatorId"     bigint,
    "Status"        int DEFAULT 0                                                  NOT NULL,
    "IsDeleted"     boolean DEFAULT false                                          NOT NULL, 
    "CreatedOn"     timestamp without time zone DEFAULT (now() at time zone 'utc') NOT NULL,
    "ModifiedOn"    timestamp without time zone
);

CREATE TABLE IF NOT EXISTS "Venue"."Locations"
(
    "LocationId"    bigserial PRIMARY KEY,
    "Address"       varchar(500)                                                   NOT NULL,
    "IsDeleted"     boolean DEFAULT false                                          NOT NULL,
    "CreatedOn"     timestamp without time zone DEFAULT (now() at time zone 'utc') NOT NULL,
    "ModifiedOn"    timestamp without time zone
);

CREATE TABLE IF NOT EXISTS "Venue"."LocationCoordinates"
(
    "LocationCoordinateId"    bigserial PRIMARY KEY,
    "LocationId"              bigint                                                            NOT NULL,
    "Latitude"                double precision,    
    "Longitude"               double precision,    
    "IsDeleted"               boolean DEFAULT false                                             NOT NULL,
    "CreatedOn"               timestamp without time zone DEFAULT (now() at time zone 'utc')    NOT NULL,
    "ModifiedOn"              timestamp without time zone
);

ALTER TABLE "Venue"."Venues"
    ADD FOREIGN KEY ("LocationId") REFERENCES "Venue"."Locations" ("LocationId") ON DELETE CASCADE;

ALTER TABLE "Venue"."LocationCoordinates"
    ADD FOREIGN KEY ("LocationId") REFERENCES "Venue"."Locations" ("LocationId") ON DELETE CASCADE;

--INDEXES--
CREATE INDEX ON "Venue"."Venues" ("CategoryId");