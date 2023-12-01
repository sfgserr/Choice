using Choice.Application.Services;

namespace Choice.Infrastructure
{
    public sealed class UnitOfWorkFake : IUnitOfWork
    {
        public async Task Save() =>
            await Task.CompletedTask;
    }
}
