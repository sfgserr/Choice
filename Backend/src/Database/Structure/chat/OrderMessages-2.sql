CREATE TABLE chat."OrderMessages" (
    "MessageId" uuid PRIMARY KEY,
    "ResponseId" uuid,
    "EnrollmentDate" timestamp,
    "IsActive" boolean NOT NULL
);
