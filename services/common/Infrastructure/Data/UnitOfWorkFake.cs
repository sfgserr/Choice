using Choice.Application.Services;

namespace Choice.Infrastructure.Data
{
    public sealed class UnitOfWorkFake : IUnitOfWork
    {
        public async Task<int> SaveChanges() =>
            await Task.FromResult(1);
    }
}
