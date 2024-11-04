using Autofac;
using BuildingBlocks.Application.Events;
using BuildingBlocks.Infrastructure.Configuration;
using BuildingBlocks.Infrastructure.Events;
using MassTransit;

namespace Identity.Infrastructure.Configuration.Events
{
    internal class EventBusModule : Module
    {
        private readonly IBus _bus;

        internal EventBusModule(IBus bus)
        {
            _bus = bus;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new EventBus(_bus))
                .As<IEventBus>()
                .SingleInstance();
            
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(IBusConsumer<>))
                .InstancePerDependency()
                .FindConstructorsWith(new AllConstructorFinder());
        }
    }
}