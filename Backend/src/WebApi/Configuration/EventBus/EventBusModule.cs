using Autofac;
using BuildingBlocks.Application.Events;
using Identity.Infrastructure.Configuration.EventBus;
using MassTransit;

namespace WebApi.Configuration.EventBus
{
    public class EventBusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assemblies.Users, Assemblies.Identity)
                .Where(x => x.IsAssignableFrom(typeof(IBusConsumer<>)))
                .AsImplementedInterfaces()
                .InstancePerDependency();
            
            builder.Register(o =>
            {
                return Bus.Factory.CreateUsingInMemory(cfg =>
                {
                    cfg.ReceiveEndpoint(x =>
                    {
                        x.AddIdentityConsumers(o);
                    });
                });
            })
            .SingleInstance()
            .As<IBus>()
            .As<IBusControl>();
        }
    }
}