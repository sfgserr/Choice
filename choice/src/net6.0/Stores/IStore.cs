using System;

namespace Choice.Stores
{
    public interface IStore<T>
    {
        event Action StateChanged;

        T State { get; set; }
    }
}
