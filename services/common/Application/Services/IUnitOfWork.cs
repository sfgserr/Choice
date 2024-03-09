
namespace Choice.Application.Services
{
    public interface IUnitOfWork
    {
        Task SaveChanges();
    }
}
