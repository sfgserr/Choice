using Autofac;
using Users.Domain.OrderRequests.OrderResponses;
using Users.Infrastructure.Payments;

namespace Users.Infrastructure.Configuration.Domain
{
    internal class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PaymentService>()
                .As<IPaymentService>()
                .InstancePerLifetimeScope();
        }
    }
}