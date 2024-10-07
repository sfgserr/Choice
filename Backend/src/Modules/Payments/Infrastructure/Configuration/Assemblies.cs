using Payments.Application.Contracts;
using System.Reflection;

namespace Payments.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(IPaymentsModule).Assembly;
    }
}
