CREATE TABLE users.Users (
    Id uuid PRIMARY KEY,
    Name text NOT NULL,
    Email text NOT NULL,
    PhoneNumber text NOT NULL,
    HashedPassword text NOT NULL,
    IconUri text NOT NULL,
    IsDataFilled boolean NOT NULL,
    Address text NOT NULL,
    UserRole text REFERENCES users.Roles (Code)
);