using Choice.Commands;
using Choice.Extensions;
using Choice.Stores.Authenticators;
using System.Linq;
using System.Windows.Input;

namespace Choice.ViewModels
{
    public class LoginViewModel : ViewModeBase
    {
        public LoginViewModel(IAuthenticator authenticator)
        {
            LoginByEmailCommand = new LoginByEmailCommand(authenticator, this);
            ShowPasswordCommand = new RelayCommand((par) => IsPassword = !IsPassword);
        }

        public ICommand LoginByEmailCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public bool CanSignInByEmail => !string.IsNullOrEmpty(Email) && 
                                        !string.IsNullOrEmpty(Password);

        public bool CanSignInByPhone => !string.IsNullOrEmpty(PhoneNumber) && PhoneNumber.Length == 15;

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
                OnPropertyChanged(nameof(CanSignInByEmail));
			}
		}

        private string _password = string.Empty;

        public string Password
        {
            get => _password;
            set
            {
                Set(ref _password, value);
                OnPropertyChanged(nameof(CanSignInByEmail));
            }
        }

        private string _phoneNumber = string.Empty;

        public string PhoneNumber
        {
            get
            {
                string formattedPhone = _phoneNumber.FormatPhoneNumber();
                return formattedPhone;
            }
            set
            {
                Set(ref _phoneNumber, new string(value.Where(c => char.IsDigit(c)).ToArray()));
                OnPropertyChanged(nameof(CanSignInByPhone));
            }
        }
    }
}
