﻿using Choice.Commands;
using Choice.Extensions;
using Choice.Stores.Authenticators;
using Choice.Stores.Loaders;
using System.Linq;
using System.Windows.Input;

namespace Choice.ViewModels
{
    public class LoginByPhoneViewModel : ViewModelBase
    {
        private readonly ILoader _loader;

        public LoginByPhoneViewModel(IAuthenticator authenticator, ILoader loader)
        {
            _loader = loader;
            _loader.StateChanged += OnIsLoadingChanged;

            LoginByPhoneCommand = new LoginByPhoneCommand(this, authenticator, _loader);
            CheckCodeCommand = new CheckCodeCommand(this);
        }

        public ICommand LoginByPhoneCommand { get; }
        public ICommand CheckCodeCommand { get; }
        public bool IsLoading => _loader.State;
        public bool CanSendCode => !string.IsNullOrEmpty(PhoneNumber) && PhoneNumber.Length == 15;
        public bool CanCheckCode => !string.IsNullOrEmpty(Code);
        public bool IsPhoneNumberTextBoxVisible => !IsCodeSent;

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
                OnPropertyChanged(nameof(CanCheckCode));
            }
        }

        private string _phoneNumber = string.Empty;

        public string PhoneNumber
        {
            get => _phoneNumber.FormatPhoneNumber();
            set
            {
                Set(ref _phoneNumber, new string(value.Where(c => char.IsDigit(c)).ToArray()));
                OnPropertyChanged(nameof(CanSendCode));
            }
        }

        private void OnIsLoadingChanged()
        {
            OnPropertyChanged(nameof(IsLoading));
        }
    }
}