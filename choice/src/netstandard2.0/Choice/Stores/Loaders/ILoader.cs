using System;
using System.Threading.Tasks;

namespace Choice.Stores.Loaders
{
    public interface ILoader : IStore<bool>
    {
        Task Load(Func<Task> task);
    }
}
