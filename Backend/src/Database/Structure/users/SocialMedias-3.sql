CREATE TABLE users."SocialMedias" (
    "CompanyId" uuid NOT NULL,
    "Url" text NOT NULL,
    "Platform" text NOT NULL,
    PRIMARY KEY ("CompanyId", "Platform")
);

