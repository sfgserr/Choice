CREATE TABLE users."Clients" (
    "Id" uuid PRIMARY KEY,
    "UserId" uuid REFERENCES users."Users" ("Id")
);