CREATE TABLE users.Reviews (
    ResponseId uuid,
    AuthorId uuid,
    ToUserId uuid,
    Text text NOT NULL,
    Grade integer NOT NULL,
    PRIMARY KEY (ResponseId, AuthorId, ToUserId)
);