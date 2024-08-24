using BuildingBlocks.Application.Cqrs.Commands;
using System.Reflection;

namespace BuildingBlocks.Infrastructure
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(ICommandHandler<>).Assembly;
    }
}
