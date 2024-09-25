using Autofac;
using BuildingBlocks.Application.Events;
using BuildingBlocks.Infrastructure.Configuration;

namespace Identity.Infrastructure.Configuration.EventBus
{
    internal class EventBusModule : Module
    {
        private readonly IEventBus _eventBus;

        internal EventBusModule(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_eventBus)
                .As<IEventBus>()
                .SingleInstance();
            
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(IBusConsumer<>))
                .InstancePerDependency()
                .FindConstructorsWith(new AllConstructorFinder());
        }
    }
}