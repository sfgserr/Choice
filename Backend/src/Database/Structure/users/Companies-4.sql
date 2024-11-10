CREATE TABLE users."Companies" (
    "Id" uuid PRIMARY KEY,
    "UserId" uuid NOT NULL,
    "Description" text NOT NULL,
    "IsPrepaymentAvailable" boolean NOT NULL,
    "PhotoUris" text[] NOT NULL,
    "SocialMediaUris" text[] NOT NULL,
    "CategoriesId" integer[] NOT NULL,
    FOREIGN KEY ("UserId") REFERENCES users."Users" ("Id")
);
