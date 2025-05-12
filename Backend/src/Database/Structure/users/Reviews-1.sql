CREATE TABLE users."Reviews" (
    "Id" uuid PRIMARY KEY,
    "ResponseId" uuid,
    "AuthorId" uuid,
    "ToUserId" uuid,
    "Text" text NOT NULL,
    "Grade" integer NOT NULL
);