using Autofac.Core.Activators.Reflection;
using System.Collections.Concurrent;
using System.Reflection;

namespace BuildingBlocks.Infrastructure.Configuration
{
    public class AllConstructorFinder : IConstructorFinder
    {
        private static ConcurrentDictionary<Type, ConstructorInfo[]> Cache =
            new ConcurrentDictionary<Type, ConstructorInfo[]>();

        public ConstructorInfo[] FindConstructors(Type targetType)
        {
            var constructors = Cache.GetOrAdd(targetType, t => t.GetTypeInfo().DeclaredConstructors.ToArray());

            return constructors.Length > 0 ? constructors : throw new NoConstructorsFoundException(targetType, this);
        }
    }
}
