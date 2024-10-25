CREATE TABLE users."Clients" (
    "Id" uuid PRIMARY KEY,
    FOREIGN KEY ("Id") REFERENCES users."Users" ("Id")
);
