CREATE TABLE users.OrderResponses (
    Id uuid REFERENCES users.OrderRequests (Id),
    ClientId uuid REFERENCES users.Clients (Id),
    CompanyId uuid REFERENCES users.Companies (Id),
    Price real NOT NULL,
    Deadline integer NOT NULL,
    EnrollmentDate timestamp NOT NULL,
    Prepayment real NOT NULL,
    Status text NOT NULL,
    IsEnrolled boolean NOT NULL,
    IsPaid boolean NOT NULL,
    UserChangedEnrollmentDate uuid,
    IsEnrollmentDateConfirmed boolean NOT NULL,
    IsActive boolean NOT NULL
    PRIMARY KEY (Id)
);