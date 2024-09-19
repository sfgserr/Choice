using Autofac;
using BuildingBlocks.Application.Events;

namespace Users.Infrastructure.Configuration.EventBus
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
        }
    }
}
