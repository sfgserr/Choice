CREATE TABLE payments."Subscriptions" (
    "Id" uuid PRIMARY KEY,
    "SubscriberId" uuid NOT NULL,
    "PeriodName" text NOT NULL,
    "PeriodCost" decimal NOT NULL,
    "PeriodCostCurrency" text NOT NULL,
    "Status" text NOT NULL,
    "ExpirationDate" timestamp NOT NULL
);
