using Choice.ClientService.Application.Services;

namespace Choice.ClientService.Infrastructure.Data
{
    public sealed class UnitOfWorkFake : IUnitOfWork
    {
        public async Task SaveChanges()
        {
            await Task.CompletedTask;
        }
    }
}
