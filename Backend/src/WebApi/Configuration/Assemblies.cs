using System.Reflection;
using Identity.Infrastructure.Configuration;
using Users.Infrastructure.Configuration;

namespace WebApi.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Users = typeof(UsersStartup).Assembly;
        public static readonly Assembly Identity = typeof(IdentityStartup).Assembly;
    }
}