using Autofac;
using Payments.Application.Contracts;
using Payments.Infrastructure.YooKassa;
using Serilog;

namespace Payments.Infrastructure.Configuration.YooKassa
{
    internal class YooKassaModule : Module
    {
        private readonly IHttpClientFactory _clientFactory;

        internal YooKassaModule(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(s => _clientFactory)
                .As<IHttpClientFactory>()
                .SingleInstance();
            
            builder.Register(s => new YooKassaClient(
                    s.Resolve<IHttpClientFactory>(),
                    s.Resolve<ILogger>()))
                .As<IPaymentsGateway>()
                .InstancePerLifetimeScope();
        }
    }
}