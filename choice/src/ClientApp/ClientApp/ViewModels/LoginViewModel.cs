using Avalonia.Controls;
using ClientApp.Services.Mappers;

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
        }

        public LoginByEmailViewModel LoginByEmailView =>_loginByEmailViewModel;
        public LoginByPhoneViewModel LoginByPhoneView => _loginByPhoneViewModel;
    }
}
