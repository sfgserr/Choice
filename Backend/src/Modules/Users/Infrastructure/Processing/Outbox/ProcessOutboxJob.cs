using BuildingBlocks.Infrastructure.Quartz;
using Quartz;
using Serilog;

namespace Users.Infrastructure.Processing.Outbox
{
    internal class ProcessOutboxJob : JobBase
    {
        public ProcessOutboxJob(ILogger logger) : base(logger)
        {
        }

        protected override async Task ExecuteJob(IJobExecutionContext context)
        {
            await CommandsExecutor.ExecuteCommandAsync(new ProcessOutboxCommand());
        }
    }
}
