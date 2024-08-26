using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Users.Domain.Users;

namespace Users.Infrastructure.Data.ValueConversion
{
    internal class UserRoleToStringValueConverter : ValueConverter<UserRole, string>
    {
        public UserRoleToStringValueConverter(ConverterMappingHints? mappingHints = null) : 
            base(r => r.Value, s => UserRole.Parse(s), mappingHints)
        {
        }
    }
}
