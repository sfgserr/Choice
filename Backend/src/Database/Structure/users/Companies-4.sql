CREATE TABLE users.Companies (
    Id uuid PRIMARY KEY,
    UserId uuid REFERENCES users.Users (Id),
    Description text NOT NULL,
    IsDataFilled boolean NOT NULL,
    IsPrepaymentAvailable boolean NOT NULL
);