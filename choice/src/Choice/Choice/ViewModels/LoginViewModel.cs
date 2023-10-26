using Choice.Commands;
using Choice.Stores.Authenticators;
using System.Windows.Input;

namespace Choice.ViewModels
{
    public class LoginViewModel : ViewModeBase
    {
        public LoginViewModel(IAuthenticator authenticator)
        {
            LoginByEmailCommand = new LoginByEmailCommand(authenticator, this);
        }

        public ICommand LoginByEmailCommand { get; }
        public bool CanSignInByEmail => !string.IsNullOrEmpty(Email) && 
                                        !string.IsNullOrEmpty(Password);

        public bool IsSignInButtonByPhoneEnabled => !string.IsNullOrEmpty(PhoneNumber);

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
            get => _phoneNumber;
            set
            {
                Set(ref _phoneNumber, value);
                OnPropertyChanged(nameof(IsSignInButtonByPhoneEnabled));
            }
        }
    }
}
