using Choice.Commands;
using Choice.Dialogs;
using Choice.Stores.Authenticators;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Choice.ViewModels
{
    public class RegisterClientViewModel : ViewModelBase
    {
        public RegisterClientViewModel(IAuthenticator authenticator, IAlertDialogService dialogService) 
        {
            RegisterClientCommand = new RegisterClientCommand(this, authenticator, dialogService);
            NavigateBackCommand = new RelayCommand(async par => await Shell.Current.GoToAsync("../"));
            ShowPasswordCommand = new RelayCommand((par) =>
            {
                string entry = (string)par;

                switch(entry)
                {
                    case "Confirmtion":
                        IsPasswordConfirmtion = !IsPasswordConfirmtion;
                        break;
                    case "Password":
                        IsPassword = !IsPassword;
                        break;
                    default:
                        throw new ArgumentException();
                }
            });
        }

        public ICommand RegisterClientCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand NavigateBackCommand { get; }
        public bool CanRegister => !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(PasswordConfirmtion)
                                         && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Name)
                                         && !string.IsNullOrEmpty(Surname);

        private bool _isPassword = true;

        public bool IsPassword
        {
            get => _isPassword; 
            set => Set(ref _isPassword, value);
        }

        private bool _isPasswordConfirmtion = true;

        public bool IsPasswordConfirmtion
        {
            get => _isPasswordConfirmtion;
            set => Set(ref _isPasswordConfirmtion, value);
        }

        private string _name = string.Empty;

        public string Name
        {
            get => _name;
            set
            {
                Set(ref _name, value);
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        private string _surname = string.Empty;

        public string Surname
        {
            get => _surname;
            set
            {
                Set(ref _surname, value);
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        private string _email = string.Empty;

        public string Email
        {
            get => _email;
            set
            {
                Set(ref _email, value);
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        private string _password = string.Empty;

		public string Password
		{
			get => _password;
			set
            {
                Set(ref _password, value);
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        private string _passwordConfirmtion = string.Empty;

        public string PasswordConfirmtion
        {
            get => _passwordConfirmtion;
            set
            {
                Set(ref _passwordConfirmtion, value);
                OnPropertyChanged(nameof(CanRegister));
            }
        }
    }
}
