using Choice.Application.Services;

namespace Choice.Infrastructure.Data
{
    public sealed class UnitOfWorkFake : IUnitOfWork
    {
        public async Task SaveChanges() =>
            await Task.CompletedTask;
    }
}
