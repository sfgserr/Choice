using Payments.Application.Subscriptions.Commands.Expire;
using Payments.Infrastructure.Processing;
using Quartz;

namespace Payments.Infrastructure.Configuration.Quartz.Jobs
{
    [DisallowConcurrentExecution]
    internal class ExpireSubscriptionsJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await CommandsExecutor.ExecuteCommandAsync(new ExpireCommand());
        }
    }
}
