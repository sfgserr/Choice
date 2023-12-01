using Choice.Stores.Authenticators;
using Choice.Stores.Loaders;
using Choice.ViewModels;
using System.ComponentModel;
using System.Windows.Input;

namespace Choice.Commands
{
    public class LoginByEmailCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly IAuthenticator _authenticator;
        private readonly ILoader _loader;
        private readonly LoginByEmailViewModel _viewModel;

        public LoginByEmailCommand(IAuthenticator authenticator, LoginByEmailViewModel viewModel, ILoader loader)
        {
            _authenticator = authenticator;
            _loader = loader;
            _viewModel = viewModel;

            _viewModel.PropertyChanged += OnCanExecuteChanged;
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.CanLogin;
        }

        public async void Execute(object parameter)
        {
            try
            {
                await _loader.Load(Login);
            }
            catch (Exception ex)
            {
                _loader.State = false;
                await Application.Current.MainPage.DisplayAlert("Внимание", ex.Message, "OK");
            }
        }

        private async Task Login()
        {
            await _authenticator.LoginByEmail(_viewModel.Email, _viewModel.Password);
            await Shell.Current.GoToAsync("//CategoryPage");
        }

        private void OnCanExecuteChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.CanLogin)) CanExecuteChanged?.Invoke(sender, e);
        }
    }
}
