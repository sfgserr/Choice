using Autofac;

namespace Chat.Infrastructure.Configuration.Mediation
{
    internal class ServiceProviderWrapper : IServiceProvider
    {
        private readonly ILifetimeScope _scope;

        internal ServiceProviderWrapper(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public object? GetService(Type serviceType) =>
            _scope.ResolveOptional(serviceType);
    }
}