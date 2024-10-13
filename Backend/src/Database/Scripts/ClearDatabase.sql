TRUNCATE administration."Categories" CASCADE;

TRUNCATE chat."Messages" CASCADE;

TRUNCATE chat."OrderMessages" CASCADE;

TRUNCATE chat."InternalCommands" CASCADE;

TRUNCATE chat."OutboxMessages" CASCADE;

TRUNCATE chat."ChatUsers" CASCADE;

TRUNCATE identity."Users" CASCADE;

TRUNCATE identity."InternalCommands" CASCADE;

TRUNCATE payments."EnrollmentPayments" CASCADE;

TRUNCATE payments."InternalCommands" CASCADE;

TRUNCATE payments."OutboxMessages" CASCADE;

TRUNCATE payments."SubscriptionPayments" CASCADE;

TRUNCATE users."OrderResponses" CASCADE;

TRUNCATE users."OrderRequests" CASCADE;

TRUNCATE users."Companies" CASCADE;

TRUNCATE users."Clients" CASCADE;

TRUNCATE users."Users" CASCADE;

TRUNCATE users."Reviews" CASCADE;

TRUNCATE users."OutboxMessages" CASCADE;

TRUNCATE users."InternalCommands" CASCADE;
