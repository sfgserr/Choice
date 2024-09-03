CREATE TABLE payments."EnrollmentPayments" (
    "Id" uuid PRIMARY KEY,
    "PayerId" uuid NOT NULL,
    "ResponseId" uuid NOT NULL,
    "Status" text NOT NULL,
    "Cost" decimal NOT NULL,
    "Currency" text NOT NULL
);