using Choice.Dialogs;
using Choice.Stores.Authenticators;
using Choice.Stores.Loaders;
using Choice.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Choice.Commands
{
    public class RegisterClientCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly RegisterClientViewModel _viewModel;
        private readonly IAlertDialogService _dialogService;
        private readonly IAuthenticator _authenticator;
        private readonly ILoader _loader;

        public RegisterClientCommand(RegisterClientViewModel viewModel, IAuthenticator authenticator, IAlertDialogService dialogService, ILoader loader)
        {
            _loader = loader;
            _authenticator = authenticator;
            _dialogService = dialogService;
            _viewModel = viewModel;

            _viewModel.PropertyChanged += OnCanExecuteChanged;
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.CanRegister;
        }

        public async void Execute(object parameter)
        {
            try
            {
                await _loader.Load(RegisterClient);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Внимание", ex.Message, "OK");
            }
        }

        private async Task RegisterClient()
        {
            await _authenticator.RegisterClient(_viewModel.Name, _viewModel.Surname, _viewModel.Email, _viewModel.Password,
                                                _viewModel.PasswordConfirmtion);
            await _dialogService.ShowDialogAsync("Аккаунт создан", "Теперь вы можете создавать заказы", "Ок", async () => await Shell.Current.GoToAsync("../"));
        }

        private void OnCanExecuteChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.CanRegister)) CanExecuteChanged?.Invoke(this, e);
        }
    }
}
