CREATE TABLE users."Users" (
    "Id" uuid PRIMARY KEY,
    "Name" text NOT NULL,
    "Email" text NOT NULL,
    "PhoneNumber" text NOT NULL,
    "IconUri" text NOT NULL,
    "IsDataFilled" boolean NOT NULL,
    "Role" text NOT NULL,
    "Street" text NOT NULL,
    "City" text NOT NULL,
    "Latitude" text NOT NULL,
    "Longitude" text NOT NULL,
    "ReviewsCount" integer NOT NULL,
    "AverageGrade" real NOT NULL
);
