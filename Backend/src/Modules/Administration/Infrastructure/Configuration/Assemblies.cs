using System.Reflection;
using Administration.Application.Contracts;

namespace Administration.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(IAdministrationModule).Assembly;
    }
}