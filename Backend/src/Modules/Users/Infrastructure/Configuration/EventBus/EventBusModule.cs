using Autofac;
using BuildingBlocks.Application.Events;
using BuildingBlocks.Infrastructure.Events;

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
            builder.Register(c => _eventBus)
                .AsSelf()
                .SingleInstance();
        }
    }
}
