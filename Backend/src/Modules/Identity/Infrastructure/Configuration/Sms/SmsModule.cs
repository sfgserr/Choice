using Autofac;
using Identity.Application.Contracts;
using Identity.Infrastructure.Sms;

namespace Identity.Infrastructure.Configuration.Sms
{
    internal class SmsModule : Module
    {
        private readonly IHttpClientFactory _clientFactory;

        internal SmsModule(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(s => _clientFactory)
                .As<IHttpClientFactory>()
                .SingleInstance();
            
            builder.Register(s => new SmsService(s.Resolve<IHttpClientFactory>()))
                .As<ISmsService>()
                .InstancePerLifetimeScope();
        }
    }
}