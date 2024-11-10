CREATE TABLE chat."OrderMessages" (
    "MessageId" uuid PRIMARY KEY,
    "ResponseId" uuid NOT NULL,
    "EnrollmentDate" timestamp,
    "IsActive" boolean NOT NULL
);
