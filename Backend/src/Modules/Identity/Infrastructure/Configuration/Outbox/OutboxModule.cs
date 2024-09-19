using Autofac;
using BuildingBlocks.Infrastructure.Outbox;
using Identity.Infrastructure.Data.Outbox;

namespace Identity.Infrastructure.Configuration.Outbox
{
    internal class OutboxModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OutboxAccessor>()
                .As<IOutbox>()
                .InstancePerLifetimeScope();
        }
    }
}