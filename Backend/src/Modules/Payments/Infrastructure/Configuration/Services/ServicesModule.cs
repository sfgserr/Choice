using Autofac;
using Payments.Application.Services;

namespace Payments.Infrastructure.Configuration.Services
{
    internal class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PaymentsService>()
                .AsSelf()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<PayoutService>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}