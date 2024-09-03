CREATE TABLE chat."Messages" (
    "Id" uuid PRIMARY KEY,
    "Type" text NOT NULL,
    "Body" text,
    "OrderMessageResponseId" uuid,
    "OrderMessageMessageId" uuid,
    "FromUserId" uuid REFERENCES chat."ChatUsers" ("Id"),
    "ToUserId" uuid REFERENCES chat."ChatUsers" ("Id"),
    "CreationDate" timestamp NOT NULL,
    FOREIGN KEY ("OrderMessageMessageId", "OrderMessageResponseId") REFERENCES chat."OrderMessages" ("MessageId", "ResponseId")
);