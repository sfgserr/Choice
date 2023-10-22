using Choice.Stores.Navigators;
using Choice.ViewModels.Factories;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Windows.Input;

namespace Choice.Commands
{
    public class UpdateCurrentViewCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly INavigator _navigator;
        private readonly IViewModelFactory _factory;

        public UpdateCurrentViewCommand(IViewModelFactory factory, INavigator navigator)
        {
            _factory = factory;
            _navigator = navigator;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is ViewType viewType)
            {
                _navigator.State = _factory.CreateViewModel(viewType);
            }
        }
    }
}
