CREATE TABLE chat."ChatUsers" (
    "Id" uuid PRIMARY KEY,
    "Status" text NOT NULL,
    "LastTimeOnline" timestamp,
    "IsDeleted" boolean NOT NULL
);