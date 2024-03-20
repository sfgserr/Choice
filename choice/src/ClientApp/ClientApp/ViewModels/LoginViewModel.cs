using Avalonia.Controls;
using ClientApp.Services.Mappers;
using ReactiveUI;

namespace ClientApp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly LoginByEmailViewModel _loginByEmailViewModel;
        private readonly LoginByPhoneViewModel _loginByPhoneViewModel;

        public LoginViewModel(LoginByEmailViewModel byEmail, LoginByPhoneViewModel byPhone)
        {
            _loginByEmailViewModel = byEmail;
            _loginByPhoneViewModel = byPhone;

            CurrentTab = byEmail;
        }

        private ViewModelBase _currentTab;

        public ViewModelBase CurrentTab
        {
            get => _currentTab;
            set => this.RaiseAndSetIfChanged(ref _currentTab, value);
        }
    }
}
