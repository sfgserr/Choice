CREATE TABLE payments."OutboxMessages" (
    "Id" uuid PRIMARY KEY,
    "Type" text NOT NULL,
    "Message" text NOT NULL,
    "OccuredOn" timestamp NOT NULL,
    "Processed" timestamp
);