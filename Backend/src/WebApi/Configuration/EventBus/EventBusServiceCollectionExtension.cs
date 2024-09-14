using BuildingBlocks.Application.Events;
using BuildingBlocks.Infrastructure.Events;
using MassTransit;

namespace WebApi.Configuration.Eventbus
{
    public static class EventBusServiceCollectionExtension
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services)
        {
            services.AddTransient<ProcessEventJob>();
            services.AddSingleton<InMemoryQueue>();
            services.AddSingleton<IEventBus, EventBus>();
            
            services.AddMassTransit(o =>
            {
                o.UsingInMemory();
            });
            
            EventBusStartup.Initialize();

            return services;
        }
    }
}