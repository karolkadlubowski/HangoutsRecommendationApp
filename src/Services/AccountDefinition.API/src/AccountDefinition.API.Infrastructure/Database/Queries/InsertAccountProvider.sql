INSERT INTO "AccountDefinition"."AccountProviders" ("Provider", "CreatedOn")
VALUES (@Provider, @CreatedOn)
RETURNING "AccountProviderId", "Provider", "CreatedOn";