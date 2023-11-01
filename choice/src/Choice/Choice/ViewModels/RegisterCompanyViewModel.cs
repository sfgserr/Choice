using Choice.Commands;
using Choice.Dialogs;
using Choice.Domain.Models;
using Choice.Services.ApiServices;
using Choice.Stores.Loaders;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Choice.ViewModels
{
    public class RegisterCompanyViewModel : ViewModelBase
    {
        private readonly ILoader _loader;

        public RegisterCompanyViewModel(IAlertDialogService alertDialogService, IApiService<Company> companyService, ILoader loader)
        {
            _loader = loader;
            _loader.StateChanged += OnIsLoadingChanged;

            NavigateBackCommand = new RelayCommand(async par => await Shell.Current.GoToAsync("../"));
            RegisterCompanyCommand = new RegisterCompanyCommand(this, alertDialogService, companyService, _loader);
            ShowPasswordCommand = new RelayCommand((par) =>
            {
                string entry = (string)par;

                switch (entry)
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

        public ICommand RegisterCompanyCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand NavigateBackCommand { get; }
        public bool IsLoading => _loader.State;
        public bool CanRegister => !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Email) &&
                                   !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(PasswordConfirmtion);

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

        private string _title = string.Empty;

        public string Title
        {
            get => _title;
            set
            {
                Set(ref _title, value);
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

        private void OnIsLoadingChanged()
        {
            OnPropertyChanged(nameof(IsLoading));
        }
    }
}
