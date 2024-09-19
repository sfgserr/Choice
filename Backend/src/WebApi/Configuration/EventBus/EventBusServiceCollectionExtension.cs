using BuildingBlocks.Application.Events;
using BuildingBlocks.Infrastructure.Events;
using Identity.Infrastructure.Configuration.EventBus;
using MassTransit;
using Quartz;

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
                o.AddIdentityConsumers();
                
                o.UsingInMemory((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });
            });

            services.AddQuartz(q =>
            {
                var jobKey = new JobKey("ProcessEventJob");
                q.AddJob<ProcessEventJob>(opt => opt.WithIdentity(jobKey));

                q.AddTrigger(opt => opt
                    .ForJob(jobKey)
                    .StartNow()
                    .WithCronSchedule("0/2 * * ? * *"));
            });

            services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);
            
            return services;
        }
    }
}