CREATE TABLE identity."Users" (
    "Id" uuid PRIMARY KEY,
    "Email" text NOT NULL,
    "PhoneNumber" text NOT NULL,
    "HashedPassword" text NOT NULL,
    "Role" text REFERENCES identity."Roles" ("Code"),
    "Street" text NOT NULL,
    "City" text NOT NULL,
    "Latitude" text NOT NULL,
    "Longitude" text NOT NULL,
    "IsSubscribed" boolean NOT NULL
);
