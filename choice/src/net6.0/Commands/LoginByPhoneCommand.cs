using Choice.Stores.Authenticators;
using Choice.Stores.Loaders;
using Choice.ViewModels;
using System.ComponentModel;
using System.Windows.Input;

namespace Choice.Commands
{
    public class LoginByPhoneCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly LoginByPhoneViewModel _viewModel;
        private readonly IAuthenticator _authenticator;
        private readonly ILoader _loader;

        public LoginByPhoneCommand(LoginByPhoneViewModel viewModel, IAuthenticator authenticator, ILoader loader)
        {
            _authenticator = authenticator;
            _viewModel = viewModel;
            _loader = loader;

            _viewModel.PropertyChanged += OnCanExecuteChanged;
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.CanSendCode;
        }

        public async void Execute(object parameter)
        {
            try
            {
                await _loader.Load(LoginByPhone);
            }
            catch (Exception ex)
            {
                _loader.State = false;
                await Application.Current.MainPage.DisplayAlert("Внимание", ex.Message, "OK");
            }
        }

        private async Task LoginByPhone()
        {
            string phoneNumber = new string(_viewModel.PhoneNumber.Where(c => char.IsDigit(c)).ToArray());
            await _authenticator.LoginByPhone(phoneNumber);
            _viewModel.IsCodeSent = true;
        }

        private void OnCanExecuteChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.CanSendCode)) CanExecuteChanged?.Invoke(this, e);
        }
    }
}
