using Autofac;
using Identity.Application.Contracts;
using Identity.Infrastructure.Sms;

namespace Identity.Infrastructure.Configuration.Sms
{
    internal class SmsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(s => new SmsService(s.Resolve<IHttpClientFactory>()))
                .As<ISmsService>()
                .InstancePerLifetimeScope();
        }
    }
}