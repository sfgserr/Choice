CREATE TABLE users."Clients" (
    "Id" uuid PRIMARY KEY,
    "UserId" uuid NOT NULL,
    FOREIGN KEY ("UserId") REFERENCES users."Users" ("Id")
);
