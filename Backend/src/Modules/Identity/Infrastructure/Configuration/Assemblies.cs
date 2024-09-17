using System.Reflection;
using Identity.Application.Contracts;

namespace Identity.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static Assembly Application { get; } = typeof(IIdentityModule).Assembly;
    }
}