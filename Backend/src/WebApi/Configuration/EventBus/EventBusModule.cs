using Autofac;
using Identity.Infrastructure.Configuration.Events;
using MassTransit;
using Payments.Infrastructure.Configuration.Events;
using Users.Infrastructure.Configuration.Events;

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
                        x.AddUsersConsumers();
                        x.AddPaymentsConsumers();
                    });
                });
            })
            .SingleInstance()
            .As<IBus>()
            .As<IBusControl>();
        }
    }
}