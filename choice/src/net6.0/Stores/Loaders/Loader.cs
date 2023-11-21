using System;
using System.Threading.Tasks;

namespace Choice.Stores.Loaders
{
    public class Loader : ILoader
    {
        public event Action StateChanged;

        private bool _state;

        public bool State
        {
            get => _state;
            set
            {
                _state = value;
                StateChanged?.Invoke();
            }
        }

        public async Task Load(Func<Task> task)
        {
            State = true;

            await task();

            State = false;
        }
    }
}
