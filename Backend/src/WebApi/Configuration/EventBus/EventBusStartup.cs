using System.Collections.Specialized;
using BuildingBlocks.Infrastructure.Events;
using Quartz;
using Quartz.Impl;

namespace WebApi.Configuration.Eventbus
{
    public static class EventBusStartup
    {
        public static void Initialize()
        {
            var configuration = new NameValueCollection
            {
                { "quartz.scheduler.instanceName", "App" }
            };

            var factory = new StdSchedulerFactory(configuration);

            IScheduler scheduler = factory.GetScheduler().GetAwaiter().GetResult();

            scheduler.Start().GetAwaiter().GetResult();

            var eventJob = JobBuilder.Create<ProcessEventJob>().Build();

            var eventJobTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithCronSchedule("0/2 * * ? * *")
                .Build();

            scheduler.ScheduleJob(eventJob, eventJobTrigger).GetAwaiter().GetResult();
        }
    }
}