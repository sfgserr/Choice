using Autofac;
using Payments.Application.Contracts;
using PaymentsModuleExternal = Payments.Infrastructure.PaymentsModule;

namespace Users.Infrastructure.Configuration.Payments
{
    internal class PaymentsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PaymentsModuleExternal>()
                .As<IPaymentsModule>()
                .InstancePerLifetimeScope();
        }
    }
}