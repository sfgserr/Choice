
namespace Choice.ClientService.Application.Services
{
    public interface IUnitOfWork
    {
        Task SaveChanges();
    }
}
