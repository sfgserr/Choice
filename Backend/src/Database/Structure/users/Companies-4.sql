CREATE TABLE users."Companies" (
    "Id" uuid PRIMARY KEY,
    "Description" text NOT NULL,
    "IsPrepaymentAvailable" boolean NOT NULL,
    "PhotoUris" text[] NOT NULL,
    "SocialMediaUris" text[] NOT NULL,
    "CategoriesId" integer[] NOT NULL,
    FOREIGN KEY ("Id") REFERENCES users."Users" ("Id")
);
