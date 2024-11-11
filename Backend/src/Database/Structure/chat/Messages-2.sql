CREATE TABLE chat."Messages" (
    "Id" uuid PRIMARY KEY,
    "Type" text NOT NULL,
    "Body" text,
    "FromUserId" uuid NOT NULL,
    "ToUserId" uuid NOT NULL,
    "CreationDate" timestamp NOT NULL
);
