CREATE TABLE users."Companies" (
    "Id" uuid PRIMARY KEY,
    "UserId" uuid REFERENCES users."Users" ("Id"),
    "Description" text NOT NULL,
    "IsPrepaymentAvailable" boolean NOT NULL,
    "CategoriesId" integer[] NOT NULL
);
