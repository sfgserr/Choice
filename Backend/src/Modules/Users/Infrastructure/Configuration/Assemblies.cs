using System.Reflection;
using Users.Application.Contracts;

namespace Users.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(IUsersModule).Assembly;
    }
}
