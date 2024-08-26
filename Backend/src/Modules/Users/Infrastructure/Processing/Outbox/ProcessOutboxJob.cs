using Quartz;

namespace Users.Infrastructure.Processing.Outbox
{
    internal class ProcessOutboxJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
