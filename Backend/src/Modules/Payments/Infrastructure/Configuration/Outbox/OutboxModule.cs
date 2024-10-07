using Autofac;
using BuildingBlocks.Infrastructure.Configuration;
using BuildingBlocks.Infrastructure.Outbox;
using Payments.Infrastructure.Data.Outbox;

namespace Payments.Infrastructure.Configuration.Outbox
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
