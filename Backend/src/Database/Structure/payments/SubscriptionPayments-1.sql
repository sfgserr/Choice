CREATE TABLE payments."SubscriptionPayments" (
    "Id" uuid PRIMARY KEY,
    "PeriodName" text NOT NULL,
    "PeriodCost" text NOT NULL,
    "PeriodCostCurrency" text NOT NULL,
    "PayerId" uuid NOT NULL,
    "Status" text NOT NULL,
    "ExpirationDate" timestamp NOT NULL
);
