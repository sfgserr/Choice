using Quartz.Impl;
using Quartz;
using System.Collections.Specialized;
using Payments.Infrastructure.Processing.Outbox;
using Payments.Infrastructure.Processing.InternalCommands;
using Payments.Infrastructure.Configuration.Quartz.Jobs;

namespace Payments.Infrastructure.Configuration.Quartz
{
    internal class QuartzStartup
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

            ScheduleProcessOutboxJob(scheduler);
            ScheduleProcessInternalCommandsJob(scheduler);
            ScheduleExpireSubscriptionPaymentsJob(scheduler);
            ScheduleExpireSubscriptionsJob(scheduler);
        }

        private static void ScheduleExpireSubscriptionsJob(IScheduler scheduler)
        {
            var expireJob = JobBuilder.Create<ExpireSubscriptionsJob>().Build();

            var expireJobTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithCronSchedule("0/59 * * ? * *")
                .Build();

            scheduler.ScheduleJob(expireJob, expireJobTrigger).GetAwaiter().GetResult();
        }

        private static void ScheduleExpireSubscriptionPaymentsJob(IScheduler scheduler)
        {
            var expireJob = JobBuilder.Create<ExpireSubscriptionPaymentsJob>().Build();

            var expireJobTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithCronSchedule("0/59 * * ? * *")
                .Build();

            scheduler.ScheduleJob(expireJob, expireJobTrigger).GetAwaiter().GetResult();
        }

        private static void ScheduleProcessOutboxJob(IScheduler scheduler)
        {
            var outboxJob = JobBuilder.Create<ProcessOutboxJob>().Build();

            var outboxJobTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithCronSchedule("0/2 * * ? * *")
                .Build();

            scheduler.ScheduleJob(outboxJob, outboxJobTrigger).GetAwaiter().GetResult();
        }

        private static void ScheduleProcessInternalCommandsJob(IScheduler scheduler)
        {
            var internalCommandsJob = JobBuilder.Create<ProcessInternalCommandsJob>().Build();

            var internalCommandsTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithCronSchedule("0/2 * * ? * *")
            .Build();

            scheduler.ScheduleJob(internalCommandsJob, internalCommandsTrigger).GetAwaiter().GetResult();
        }
    }
}
