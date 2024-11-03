using System.Reflection;
using Chat.Application.Contracts;

namespace Chat.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(IChatModule).Assembly;
    }
}