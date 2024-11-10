using Quartz.Impl;
using Quartz;
using System.Collections.Specialized;
using Autofac;
using BuildingBlocks.Infrastructure.Quartz;
using Serilog;
using Users.Infrastructure.Processing.Outbox;
using Users.Infrastructure.Processing.InternalCommands;

namespace Users.Infrastructure.Configuration.Quartz
{
    internal class QuartzStartup
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

            var outboxJob = JobBuilder.Create<ProcessOutboxJob>().Build();

            var outboxJobTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithCronSchedule("0/4 * * ? * *")
                .Build();

            scheduler.ScheduleJob(outboxJob, outboxJobTrigger).GetAwaiter().GetResult();

            var internalCommandsJob = JobBuilder.Create<ProcessInternalCommandsJob>().Build();

            var internalCommandsTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithCronSchedule("0/4 * * ? * *")
                .Build();

            scheduler.ScheduleJob(internalCommandsJob, internalCommandsTrigger).GetAwaiter().GetResult();
        }
    }
}
