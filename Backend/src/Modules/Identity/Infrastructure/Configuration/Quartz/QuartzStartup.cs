using System.Collections.Specialized;
using Autofac;
using BuildingBlocks.Infrastructure.Quartz;
using Identity.Infrastructure.Processing.InternalCommands;
using Quartz;
using Quartz.Impl;
using Serilog;

namespace Identity.Infrastructure.Configuration.Quartz
{
    internal static class QuartzStartup
    {
        public static void Initialize(ILogger logger)
        {
            var configuration = new NameValueCollection
            {
                { "quartz.scheduler.instanceName", "App" }
            };

            var factory = new StdSchedulerFactory(configuration);
            
            var scheduler = factory.GetScheduler().GetAwaiter().GetResult();
            scheduler.JobFactory = new AutofacJobFactory(logger);
            
            scheduler.Start().GetAwaiter().GetResult();

            var job = JobBuilder.Create<ProcessInternalCommandsJob>().Build();

            var jobTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithCronSchedule("0/4 * * ? * *")
                .Build();

            scheduler.ScheduleJob(job, jobTrigger).GetAwaiter().GetResult();
        }
    }
}