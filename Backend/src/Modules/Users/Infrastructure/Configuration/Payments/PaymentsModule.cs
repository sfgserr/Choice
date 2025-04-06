using Autofac;
using Payments.Application.Contracts;

namespace Users.Infrastructure.Configuration.Payments
{
    internal class PaymentsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PaymentsModule>()
                .As<IPaymentsModule>()
                .InstancePerLifetimeScope();
        }
    }
}