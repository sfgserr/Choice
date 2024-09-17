using System.Collections.Specialized;
using Identity.Infrastructure.Processing.InternalCommands;
using Quartz;
using Quartz.Impl;

namespace Identity.Infrastructure.Configuration.Quartz
{
    internal static class QuartzStartup
    {
        public static void Initialize()
        {
            var configuration = new NameValueCollection
            {
                { "quartz.scheduler.instanceName", "App" }
            };

            var factory = new StdSchedulerFactory(configuration);

            var scheduler = factory.GetScheduler().GetAwaiter().GetResult();
            
            scheduler.Start().GetAwaiter().GetResult();

            var job = JobBuilder.Create<ProcessInternalCommandsJob>().Build();

            var jobTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithCronSchedule("0/2 * * ? * *")
                .Build();

            scheduler.ScheduleJob(job, jobTrigger).GetAwaiter().GetResult();
        }
    }
}