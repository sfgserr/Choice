CREATE TABLE users.RolePermissions (
    RoleCode text REFERENCES users.Roles (Code),
    PermissionCode text REFERENCES users.Permissions (Code),
    PRIMARY KEY (RoleCode, PermissionCode)
);