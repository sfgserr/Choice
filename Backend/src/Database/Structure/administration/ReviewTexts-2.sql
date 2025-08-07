CREATE TABLE administration."ReviewTexts" (
    "Id" bigint GENERATED ALWAYS AS IDENTITY,
    "Text" text NOT NULL,
    "Grade" integer NOT NULL
);
