CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;
CREATE TABLE "Users" (
    "Id" uuid NOT NULL,
    "Username" text NOT NULL,
    "Email" text NOT NULL,
    "Password" text NOT NULL,
    "Phone" text NOT NULL,
    "UserType" text NOT NULL,
    CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250329085944_InitialCreate', '9.0.3');

ALTER TABLE "Users" ALTER COLUMN "UserType" TYPE integer;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250329093527_ChangedUserType', '9.0.3');

COMMIT;

