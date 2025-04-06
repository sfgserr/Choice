using Autofac;
using Payments.Application.Contracts;
using Payments.Infrastructure.YooKassa;

namespace Payments.Infrastructure.Configuration.YooKassa
{
    internal class YooKassaModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(s => new YooKassaClient(s.Resolve<IHttpClientFactory>()))
                .As<IPaymentsGateway>()
                .InstancePerLifetimeScope();
        }
    }
}