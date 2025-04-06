CREATE TABLE payments."Wallets" (
    "Id" uuid,
    "PayerId" uuid,
    "Copecks" integer NOT NULL,
    PRIMARY KEY ("Id", "PayerId")
);
