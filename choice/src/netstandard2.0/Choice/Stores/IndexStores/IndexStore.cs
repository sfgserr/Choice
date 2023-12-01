using System;

namespace Choice.Stores.IndexStores
{
    public class IndexStore : IIndexStore
    {
        public event Action StateChanged;

        private int _index = 1;

        public int State
        {
            get => _index;
            set
            {
                _index = value;
                StateChanged?.Invoke();
            }
        }
    }
}
