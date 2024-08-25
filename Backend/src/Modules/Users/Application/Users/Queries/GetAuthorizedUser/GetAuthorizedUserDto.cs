
namespace Users.Application.Users.Queries.GetAuthorizedUser
{
    public class GetAuthorizedUserDto
    {
        public GetAuthorizedUserDto(bool isDataFilled, List<PermissionDto> permissions)
        {
            IsDataFilled = isDataFilled;
            Permissions = permissions;
        }

        public bool IsDataFilled { get; }

        public List<PermissionDto> Permissions { get; }
    }
}
