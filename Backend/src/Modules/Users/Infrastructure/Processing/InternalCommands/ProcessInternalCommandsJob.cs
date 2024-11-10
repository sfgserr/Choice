using BuildingBlocks.Infrastructure.Quartz;
using Quartz;
using Serilog;

namespace Users.Infrastructure.Processing.InternalCommands
{
    internal class ProcessInternalCommandsJob : JobBase
    {
        public ProcessInternalCommandsJob(ILogger logger) : base(logger)
        {
        }

        protected override async Task ExecuteJob(IJobExecutionContext context)
        {
            await CommandsExecutor.ExecuteCommandAsync(new ProcessInternalCommandsCommand());
        }
    }
}
