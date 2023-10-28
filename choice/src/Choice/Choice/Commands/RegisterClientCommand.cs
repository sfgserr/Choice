using Choice.Stores.Authenticators;
using Choice.ViewModels;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Choice.Commands
{
    public class RegisterClientCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly RegisterClientViewModel _viewModel;
        private readonly IAuthenticator _authenticator;

        public RegisterClientCommand(RegisterClientViewModel viewModel, IAuthenticator authenticator)
        {   
            _authenticator = authenticator;
            _viewModel = viewModel;

            _viewModel.PropertyChanged += OnCanExecuteChanged;
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.CanRegister;
        }

        public async void Execute(object parameter)
        {
            await _authenticator.RegisterClient(_viewModel.Name, _viewModel.Surname, _viewModel.Email, _viewModel.Password,
                                                _viewModel.PasswordConfirmtion);
        }

        private void OnCanExecuteChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.CanRegister)) CanExecuteChanged?.Invoke(this, e);
        }
    }
}
