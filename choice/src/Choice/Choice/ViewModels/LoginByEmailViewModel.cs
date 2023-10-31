using Choice.Commands;
using Choice.Stores.Authenticators;
using System.Windows.Input;

namespace Choice.ViewModels
{
    public class LoginByEmailViewModel : ViewModelBase
    {
        public LoginByEmailViewModel(IAuthenticator authenticator)
        {
            LoginCommand = new LoginByEmailCommand(authenticator, this);
            ShowPasswordCommand = new RelayCommand(par => IsPassword = !IsPassword);
        }

        public ICommand LoginCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public bool CanLogin => !string.IsNullOrEmpty(Email) &&
                                !string.IsNullOrEmpty(Password);

        private bool _isPassword = true;

        public bool IsPassword
        {
            get => _isPassword;
            set => Set(ref _isPassword, value);
        }

        private string _email = string.Empty;

        public string Email
        {
            get => _email;
            set
            {
                Set(ref _email, value);
                OnPropertyChanged(nameof(CanLogin));
            }
        }

        private string _password = string.Empty;

        public string Password
        {
            get => _password;
            set
            {
                Set(ref _password, value);
                OnPropertyChanged(nameof(CanLogin));
            }
        }
    }
}
