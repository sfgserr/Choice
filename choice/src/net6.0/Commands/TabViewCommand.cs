using Choice.Stores.IndexStores;
using System.Windows.Input;

namespace Choice.Commands
{
    public class TabViewCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly IIndexStore _indexStore;

        public TabViewCommand(IIndexStore indexStore)
        {
            _indexStore = indexStore;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _indexStore.State = int.Parse(parameter.ToString());
        }
    }
}
