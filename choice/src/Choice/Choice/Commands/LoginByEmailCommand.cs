using Choice.Stores.Authenticators;
using Choice.ViewModels;
using System;
using System.Windows.Input;

namespace Choice.Commands
{
    public class LoginByEmailCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly IAuthenticator _authenticator;
        private readonly LoginViewModel _viewModel;

        public LoginByEmailCommand(IAuthenticator authenticator, LoginViewModel viewModel)
        {
            _authenticator = authenticator;
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.CanSignInByEmail;
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
