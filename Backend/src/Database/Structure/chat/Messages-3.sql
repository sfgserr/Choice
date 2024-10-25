CREATE TABLE chat."Messages" (
    "Id" uuid PRIMARY KEY,
    "Type" text NOT NULL,
    "Body" text,
    "FromUserId" uuid REFERENCES chat."ChatUsers" ("Id"),
    "ToUserId" uuid REFERENCES chat."ChatUsers" ("Id"),
    "CreationDate" timestamp NOT NULL,
    FOREIGN KEY ("Id") REFERENCES chat."OrderMessages" ("MessageId")
);
