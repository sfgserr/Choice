using Autofac;
using BuildingBlocks.Infrastructure.Configuration;
using BuildingBlocks.Infrastructure.Outbox;
using Users.Infrastructure.Data.Outbox;

namespace Users.Infrastructure.Configuration.Outbox
{
    internal class OutboxModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OutboxAccessor>()
                .As<IOutbox>()
                .InstancePerLifetimeScope()
                .FindConstructorsWith(new AllConstructorFinder());
        }
    }
}
