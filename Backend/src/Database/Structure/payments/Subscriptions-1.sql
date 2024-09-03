CREATE TABLE payments."Subscriptions" (
    "Id" uuid PRIMARY KEY,
    "PeriodName" text NOT NULL,
    "PeriodCost" text NOT NULL,
    "Status" text NOT NULL,
    "ExpirationDate" timestamp NOT NULL
);