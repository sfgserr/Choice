
namespace Users.Application.Users.Queries.GetAuthorizedUser
{
    public class PermissionDto
    {
        public PermissionDto(string code, bool isDataFillingRequired)
        {
            Code = code;
            IsDataFillingRequired = isDataFillingRequired;
        }

        public string Code { get; }

        public bool IsDataFillingRequired { get; }
    }
}
