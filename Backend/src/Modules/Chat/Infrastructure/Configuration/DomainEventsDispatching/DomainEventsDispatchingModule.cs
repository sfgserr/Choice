using Autofac;
using BuildingBlocks.Infrastructure.DomainEventDispatching;

namespace Chat.Infrastructure.Configuration.DomainEventsDispatching
{
    internal class DomainEventsDispatchingModule : Module
    {
        private readonly Dictionary<string, Type> _mappings;

        internal DomainEventsDispatchingModule(Dictionary<string, Type> mappings)
        {
            _mappings = mappings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DomainEventsDispatcher>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DomainEventsAccessor>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DomainEventsMapper>()
                .WithParameter("mappings", _mappings)
                .SingleInstance();
        }
    }
}