using Choice.Commands;
using Choice.Stores.Authenticators;
using Choice.Stores.Loaders;
using System.Windows.Input;

namespace Choice.ViewModels
{
    public class LoginByEmailViewModel : ViewModelBase
    {
        private readonly ILoader _loader;

        public LoginByEmailViewModel(IAuthenticator authenticator, ILoader loader)
        {
            _loader = loader;
            _loader.StateChanged += OnIsLoadingChanged;

            LoginCommand = new LoginByEmailCommand(authenticator, this, loader);
            ShowPasswordCommand = new RelayCommand(par => IsPassword = !IsPassword);
        }

        public ICommand LoginCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public bool IsLoading => _loader.State;
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

        private void OnIsLoadingChanged()
        {
            OnPropertyChanged(nameof(IsLoading));
        }
    }
}
