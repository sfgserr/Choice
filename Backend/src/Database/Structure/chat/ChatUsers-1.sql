CREATE TABLE chat."ChatUsers" (
    "Id" uuid PRIMARY KEY,
    "Name" text NOT NULL,
    "IconUri" text NOT NULL,
    "IsDeleted" boolean NOT NULL
);
