using System.Data;

namespace BuildingBlocks.Application.Data
{
    public interface ISqlConnectionFactory : IDisposable
    {
        IDbConnection GetConnection();
    }
}
