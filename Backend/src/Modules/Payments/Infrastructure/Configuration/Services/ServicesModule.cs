using Autofac;
using BuildingBlocks.Infrastructure.Configuration;
using Payments.Application.Services;

namespace Payments.Infrastructure.Configuration.Services
{
    internal class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PaymentsService>()
                .AsSelf()
                .InstancePerLifetimeScope()
                .FindConstructorsWith(new AllConstructorFinder());
            
            builder.RegisterType<PayoutService>()
                .AsSelf()
                .InstancePerLifetimeScope()
                .FindConstructorsWith(new AllConstructorFinder());
        }
    }
}