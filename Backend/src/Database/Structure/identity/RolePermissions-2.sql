CREATE TABLE identity."RolePermissions" (
    "RoleCode" text REFERENCES identity."Roles" ("Code"),
    "PermissionCode" text REFERENCES identity."Permissions" ("Code"),
    PRIMARY KEY ("RoleCode", "PermissionCode")
);
