using Quartz;

namespace Identity.Infrastructure.Processing.InternalCommands
{
    [DisallowConcurrentExecution]
    internal class ProcessInternalCommandsJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await CommandsExecutor.ExecuteCommandAsync(new ProcessInternalCommandsCommand());
        }
    }
}