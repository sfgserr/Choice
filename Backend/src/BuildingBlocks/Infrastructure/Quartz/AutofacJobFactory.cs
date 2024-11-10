using Autofac;
using Quartz;
using Quartz.Spi;
using Serilog;

namespace BuildingBlocks.Infrastructure.Quartz
{
    public class AutofacJobFactory : IJobFactory
    {
        private readonly ILogger _logger;

        public AutofacJobFactory(ILogger logger)
        {
            _logger = logger;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobType = bundle.JobDetail.JobType;

            if (jobType.IsAssignableTo(typeof(JobBase)))
                return (IJob)Activator.CreateInstance(jobType, _logger)!;
            
            return (IJob)Activator.CreateInstance(jobType)!;
        }

        public void ReturnJob(IJob job)
        {
            
        }
    }
}