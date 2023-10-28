using Choice.Stores.Authenticators;
using Choice.ViewModels;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Choice.Commands
{
    public class LoginByPhoneCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly LoginViewModel _viewModel;
        private readonly IAuthenticator _authenticator;

        public LoginByPhoneCommand(LoginViewModel viewModel, IAuthenticator authenticator)
        {   
            _authenticator = authenticator;
            _viewModel = viewModel;

            _viewModel.PropertyChanged += OnCanExecuteChanged;
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.CanSignInByPhone;
        }

        public async void Execute(object parameter)
        {
            try
            {
                string phoneNumber = new string(_viewModel.PhoneNumber.Where(c => char.IsDigit(c)).ToArray());
                await _authenticator.LoginByPhone(phoneNumber);
                _viewModel.IsCodeSent = true;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Внимание", ex.Message, "OK");
            }
        }

        private void OnCanExecuteChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.CanSignInByPhone)) CanExecuteChanged?.Invoke(this, e);
        }
    }
}
