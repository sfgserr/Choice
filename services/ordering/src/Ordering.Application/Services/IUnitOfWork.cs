
namespace Choice.Ordering.Application.Services
{
    public interface IUnitOfWork
    {
        Task SaveChanges();
    }
}
