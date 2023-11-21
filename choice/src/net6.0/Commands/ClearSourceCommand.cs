using Choice.ViewModels;
using System;
using System.Windows.Input;

namespace Choice.Commands
{
    public class ClearSourceCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly PhotoViewModel _viewModel;

        public ClearSourceCommand(PhotoViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.Source = string.Empty;
        }
    }
}
