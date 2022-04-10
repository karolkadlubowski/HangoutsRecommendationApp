INSERT INTO "AccountDefinition"."AccountProviders" ("Provider")
VALUES (@Provider)
RETURNING "AccountProviderId", "Provider", "CreatedOn";