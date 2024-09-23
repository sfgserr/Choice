using Autofac;
using Identity.Infrastructure.Configuration.EventBus;
using MassTransit;

namespace WebApi.Configuration.EventBus
{
    public class EventBusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(o =>
            {
                return Bus.Factory.CreateUsingInMemory(cfg =>
                {
                    cfg.ReceiveEndpoint(x =>
                    {
                        x.AddIdentityConsumers();
                    });
                });
            })
            .SingleInstance()
            .As<IBus>()
            .As<IBusControl>();
        }
    }
}