CREATE TABLE chat."InternalCommands" (
    "Id" uuid PRIMARY KEY,
    "Type" text NOT NULL,
    "Data" text NOT NULL,
    "Processed" timestamp,
    "Error" text
);