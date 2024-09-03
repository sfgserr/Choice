CREATE TABLE chat."OrderMessages" (
    "ResponseId" uuid,
    "MessageId" uuid,
    "CreationDate" timestamp,
    "IsActive" boolean NOT NULL,
    PRIMARY KEY ("ResponseId", "MessageId")
);