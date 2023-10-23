using System;
using System.Collections.Generic;
using System.Text;

namespace Choice.Stores
{
    public interface IStore<T>
    {
        event Action StateChanged;

        T State { get; set; }
    }
}
