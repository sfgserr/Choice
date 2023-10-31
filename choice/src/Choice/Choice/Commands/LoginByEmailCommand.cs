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
        private readonly LoginByEmailViewModel _viewModel;

        public LoginByEmailCommand(IAuthenticator authenticator, LoginByEmailViewModel viewModel)
        {
            _authenticator = authenticator;
            _viewModel = viewModel;

            _viewModel.PropertyChanged += OnCanExecuteChanged;
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.CanLogin;
        }

        public async void Execute(object parameter)
        {
            try
            {
                await _authenticator.LoginByEmail(_viewModel.Email, _viewModel.Password);
                await Shell.Current.GoToAsync("//MainPage");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Внимание", ex.Message, "OK");
            }
        }

        private void OnCanExecuteChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.CanLogin)) CanExecuteChanged?.Invoke(sender, e);
        }
    }
}
