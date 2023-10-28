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
            LoginByPhoneCommand = new LoginByPhoneCommand(this, authenticator);
            CheckCodeCommand = new CheckCodeCommand(this);
            ShowPasswordCommand = new RelayCommand((par) => IsPassword = !IsPassword);
            DisplayActionSheetCommand = new DisplayAccountCreationActionSheet();
        }

        public ICommand LoginByEmailCommand { get; }
        public ICommand LoginByPhoneCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand CheckCodeCommand { get; }
        public ICommand DisplayActionSheetCommand { get; }
        public bool CanSignInByEmail => !string.IsNullOrEmpty(Email) && 
                                        !string.IsNullOrEmpty(Password);

        public bool CanSignInByPhone => !string.IsNullOrEmpty(PhoneNumber) && PhoneNumber.Length == 15;
        public bool CanSignInByCode => !string.IsNullOrEmpty(Code);
        public bool IsPhoneNumberTextBoxVisible => !IsCodeSent;

        private bool _isPassword = true;

        public bool IsPassword
        {
            get => _isPassword;
            set => Set(ref _isPassword, value);
        }

        private bool _isCodeSent = false;

        public bool IsCodeSent
        {
            get => _isCodeSent;
            set
            {
                Set(ref _isCodeSent, value);
                OnPropertyChanged(nameof(IsPhoneNumberTextBoxVisible));
            }
        }

        private string _code = string.Empty;

        public string Code
        {
            get => _code;
            set
            {
                Set(ref _code, value);
                OnPropertyChanged(nameof(CanSignInByCode));
            }
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
            get => _phoneNumber.FormatPhoneNumber();
            set
            {
                Set(ref _phoneNumber, new string(value.Where(c => char.IsDigit(c)).ToArray()));
                OnPropertyChanged(nameof(CanSignInByPhone));
            }
        }
    }
}
