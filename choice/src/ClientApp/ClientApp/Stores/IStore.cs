using System;

namespace ClientApp.Stores
{
    public interface IStore<T>
    {
        event Action StateChanged;

        T State { get; }

        void SetState(T state);
    }
}
