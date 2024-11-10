using Quartz;
using Serilog;

namespace BuildingBlocks.Infrastructure.Quartz
{
    [DisallowConcurrentExecution]
    public abstract class JobBase : IJob
    {
        private readonly ILogger _logger;

        protected JobBase(ILogger logger)
        {
            _logger = logger;
        }

        protected abstract Task ExecuteJob(IJobExecutionContext context);
        
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                await ExecuteJob(context);
            }
            catch (Exception e)
            {
                _logger.Error("Job failed with error {Message}", e.Message);
                throw;
            }
        }
    }
}