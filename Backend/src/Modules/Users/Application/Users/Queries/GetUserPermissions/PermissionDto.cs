
namespace Users.Application.Users.Queries.GetUserPermissions
{
    public class PermissionDto
    {
        public PermissionDto(string code)
        {
            Code = code;
        }

        public string Code { get; }
    }
}
