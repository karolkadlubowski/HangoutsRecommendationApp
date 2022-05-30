CREATE SCHEMA IF NOT EXISTS "Identity";

CREATE TABLE IF NOT EXISTS "Identity"."Users"
(
    "UserId" bigserial NOT NULL PRIMARY KEY,
    "Email" varchar(320) NOT NULL UNIQUE,
    "PasswordHash" bytea,
    "PasswordSalt" bytea,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "CreatedOn" timestamp without time zone DEFAULT (now() at time zone 'utc') NOT NULL,
    "ModifiedOn" timestamp without time zone
    );

--INDEXES--
CREATE INDEX ON "Identity"."Users" ("Email");