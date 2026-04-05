-- Seed PlaceTypes (without IconName - derived from ID in frontend)
INSERT INTO "PlaceTypes" ("Id", "Name", "Description", "CreatedAt") VALUES ('11111111-1111-1111-1111-111111111111', 'Gym', 'Gym and fitness centers', NOW());
INSERT INTO "PlaceTypes" ("Id", "Name", "Description", "CreatedAt") VALUES ('22222222-2222-2222-2222-222222222222', 'Park', 'Public parks and outdoor areas', NOW());
INSERT INTO "PlaceTypes" ("Id", "Name", "Description", "CreatedAt") VALUES ('33333333-3333-3333-3333-333333333333', 'Stadium', 'Sports stadiums and arenas', NOW());
INSERT INTO "PlaceTypes" ("Id", "Name", "Description", "CreatedAt") VALUES ('44444444-4444-4444-4444-444444444444', 'Swimming Pool', 'Swimming pools and water sports', NOW());
INSERT INTO "PlaceTypes" ("Id", "Name", "Description", "CreatedAt") VALUES ('55555555-5555-5555-5555-555555555555', 'Tennis Court', 'Tennis courts', NOW());

-- Seed test user
INSERT INTO "Users" ("Id", "GoogleId", "UserName", "Email", "FirstName", "LastName", "CreatedAt") VALUES ('aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa', 'test-google-id', 'testuser', 'test@example.com', 'Test', 'User', NOW());
