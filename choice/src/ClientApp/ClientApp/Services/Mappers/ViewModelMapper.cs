using Avalonia.Controls;
using ClientApp.ViewModels;
using ClientApp.Views;

namespace ClientApp.Services.Mappers
{
    public class ViewModelMapper
    {
        private readonly LoginView _login;
        private readonly LoginByEmailView _loginByEmail;
        private readonly LoginByPhoneView _loginByPhone;

        public ViewModelMapper(LoginView login, LoginByEmailView loginByEmail, LoginByPhoneView loginByPhone)
        {
            _login = login;
            _loginByEmail = loginByEmail;
            _loginByPhone = loginByPhone;
        }

        public Control Map(ViewModelBase viewModel)
        {
            string viewName = viewModel.GetType().Name.Replace("ViewModel", "View");

            return viewName switch
            {
                "LoginView" => _login,
                "LoginByEmailView" => _loginByEmail,
                "LoginByPhoneView" => _loginByPhone,
                _ => new TextBlock() { Text = $"Not Found Control {viewName}" }
            };
        }
    }
}
