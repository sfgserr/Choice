using Choice.Stores.Authenticators;
using Choice.ViewModels;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

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

            _viewModel.PropertyChanged += OnCanExecuteChanged;
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.CanSignInByEmail;
        }

        public async void Execute(object parameter)
        {
            try
            {
                await _authenticator.LoginByEmail(_viewModel.Email, _viewModel.Password);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Внимание", ex.Message, "OK");
            }
        }

        private void OnCanExecuteChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.CanSignInByEmail)) CanExecuteChanged?.Invoke(sender, e);
        }
    }
}
