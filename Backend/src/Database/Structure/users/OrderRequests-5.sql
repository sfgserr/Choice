CREATE TABLE users.OrderRequests (
    Id uuid PRIMARY KEY,
    ClientCreatedId uuid REFERENCES users.Clients (Id),
    ToKnowPrice boolean NOT NULL,
    ToKnowDeadline boolean NOT NULL,
    ToKnowEnrollmentDate boolean NOT NULL,
    Distance integer NOT NULL,
    Description text NOT NULL,
    Status text NOT NULL,
    CategoryId integer NOT NULL,
    CreationDate timestamp NOT NULL
);