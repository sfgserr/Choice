
namespace Choice.Infrastructure.Data
{
    public interface IContext
    {
        Task<int> SaveEntities();
    }
}
