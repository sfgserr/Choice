using Quartz;

namespace Users.Infrastructure.Processing.Outbox
{
    [DisallowConcurrentExecution]
    internal class ProcessOutboxJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await CommandsExecutor.ExecuteCommandAsync(new ProcessOutboxCommand());
        }
    }
}
