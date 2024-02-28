using Choice.Ordering.Application.Services;

namespace Choice.Ordering.Infrastructure.Data
{
    public sealed class UnitOfWorkFake : IUnitOfWork
    {
        public async Task SaveChanges()
        {
            await Task.CompletedTask;
        }
    }
}
