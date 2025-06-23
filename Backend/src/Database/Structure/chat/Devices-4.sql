CREATE TABLE chat."Devices" (
    "UserId" uuid,
    "Name" text NOT NULL,
    "Token" text NOT NULL,
    "ExpirationDate" timestamp NOT NULL,
    PRIMARY KEY ("UserId", "Name")
);
