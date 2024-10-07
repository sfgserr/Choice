using Payments.Application.SubscriptionPayments.Commands.Expire;
using Payments.Infrastructure.Processing;
using Quartz;

namespace Payments.Infrastructure.Configuration.Quartz.Jobs
{
    internal class ExpireSubscriptionPaymentsJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await CommandsExecutor.ExecuteCommandAsync(new ExpireCommand());
        }
    }
}
