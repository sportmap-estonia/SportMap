CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;
CREATE TABLE "PrivacyTypes" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Description" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "ModifiedAt" timestamp with time zone,
    "RemovedAt" timestamp with time zone,
    CONSTRAINT "PK_PrivacyTypes" PRIMARY KEY ("Id")
);

CREATE TABLE "UserRoles" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Description" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "ModifiedAt" timestamp with time zone,
    "RemovedAt" timestamp with time zone,
    CONSTRAINT "PK_UserRoles" PRIMARY KEY ("Id")
);

CREATE TABLE "Users" (
    "Id" uuid NOT NULL,
    "GoogleId" text NOT NULL,
    "UserName" text NOT NULL,
    "Email" text NOT NULL,
    "FirstName" text NOT NULL,
    "LastName" text,
    "Birthdate" date,
    "UserRoleId" uuid,
    "PersonalizationId" uuid,
    "CreatedAt" timestamp with time zone NOT NULL,
    "ModifiedAt" timestamp with time zone,
    "RemovedAt" timestamp with time zone,
    CONSTRAINT "PK_Users" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Users_UserRoles_UserRoleId" FOREIGN KEY ("UserRoleId") REFERENCES "UserRoles" ("Id") ON DELETE SET NULL
);

CREATE TABLE "Personalization" (
    "Id" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "BirthdatePrivacyId" uuid NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "ModifiedAt" timestamp with time zone,
    "RemovedAt" timestamp with time zone,
    CONSTRAINT "PK_Personalization" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Personalization_PrivacyTypes_BirthdatePrivacyId" FOREIGN KEY ("BirthdatePrivacyId") REFERENCES "PrivacyTypes" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Personalization_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Personalization_BirthdatePrivacyId" ON "Personalization" ("BirthdatePrivacyId");

CREATE UNIQUE INDEX "IX_Personalization_UserId" ON "Personalization" ("UserId");

CREATE UNIQUE INDEX "IX_Users_Email" ON "Users" ("Email");

CREATE UNIQUE INDEX "IX_Users_GoogleId" ON "Users" ("GoogleId");

CREATE INDEX "IX_Users_UserRoleId" ON "Users" ("UserRoleId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260322153759_InitEntity', '10.0.5');

COMMIT;

START TRANSACTION;
ALTER TABLE "Users" ADD "XMin" bigint NOT NULL DEFAULT 0;

ALTER TABLE "UserRoles" ADD "XMin" bigint NOT NULL DEFAULT 0;

ALTER TABLE "PrivacyTypes" ADD "XMin" bigint NOT NULL DEFAULT 0;

ALTER TABLE "Personalization" ADD "XMin" bigint NOT NULL DEFAULT 0;

CREATE TABLE "Posts" (
    "Id" uuid NOT NULL,
    "Title" character varying(200) NOT NULL,
    "Content" text NOT NULL,
    "Status" integer NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "ModifiedAt" timestamp with time zone,
    "RemovedAt" timestamp with time zone,
    CONSTRAINT "PK_Posts" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260402084224_CreatePost', '10.0.5');

COMMIT;

START TRANSACTION;
CREATE TABLE "Images" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "FileName" text NOT NULL,
    "ContentType" text NOT NULL,
    "FileSize" bigint NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "ModifiedAt" timestamp with time zone,
    "RemovedAt" timestamp with time zone,
    CONSTRAINT "PK_Images" PRIMARY KEY ("Id")
);

CREATE TABLE "PlaceTypes" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Description" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "ModifiedAt" timestamp with time zone,
    "RemovedAt" timestamp with time zone,
    CONSTRAINT "PK_PlaceTypes" PRIMARY KEY ("Id")
);

CREATE TABLE "Places" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Description" text NOT NULL,
    "PlaceTypeId" uuid NOT NULL,
    "Latitude" double precision NOT NULL,
    "Longitude" double precision NOT NULL,
    "Address" text,
    "ImageId" uuid,
    "CreatorId" uuid NOT NULL,
    "Status" integer NOT NULL,
    "ReviewerId" uuid,
    "CreatedAt" timestamp with time zone NOT NULL,
    "ModifiedAt" timestamp with time zone,
    "RemovedAt" timestamp with time zone,
    CONSTRAINT "PK_Places" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Places_Images_ImageId" FOREIGN KEY ("ImageId") REFERENCES "Images" ("Id"),
    CONSTRAINT "FK_Places_PlaceTypes_PlaceTypeId" FOREIGN KEY ("PlaceTypeId") REFERENCES "PlaceTypes" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Places_Users_CreatorId" FOREIGN KEY ("CreatorId") REFERENCES "Users" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Places_Users_ReviewerId" FOREIGN KEY ("ReviewerId") REFERENCES "Users" ("Id")
);

CREATE INDEX "IX_Places_CreatorId" ON "Places" ("CreatorId");

CREATE INDEX "IX_Places_ImageId" ON "Places" ("ImageId");

CREATE INDEX "IX_Places_PlaceTypeId" ON "Places" ("PlaceTypeId");

CREATE INDEX "IX_Places_ReviewerId" ON "Places" ("ReviewerId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260404101301_AddPlaceEntities', '10.0.5');

COMMIT;

