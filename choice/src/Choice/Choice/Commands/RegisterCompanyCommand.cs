using Choice.Dialogs.AccountCreatedDialogs;
using Choice.Domain.Models;
using Choice.Pages;
using Choice.Services.ApiServices;
using Choice.Services.AuthenticationServices;
using Choice.Stores.Loaders;
using Choice.Validators;
using Choice.ViewModels;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Choice.Commands
{
    public class RegisterCompanyCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly RegisterCompanyViewModel _viewModel;
        private readonly IAlertDialogService _dialogService;
        private readonly IApiService<Company> _companyService;
        private readonly ILoader _loader;

        public RegisterCompanyCommand(RegisterCompanyViewModel viewModel, IAlertDialogService dialogService, IApiService<Company> companyService, ILoader loader)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnCanExecuteChanged;

            _dialogService = dialogService;
            _companyService = companyService;

            _loader = loader;
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.CanRegister;
        }

        public async void Execute(object parameter)
        {
            try
            {
                await _loader.Load(RegisterCompany);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Внимание", ex.Message, "OK");
            }
        }

        private async Task RegisterCompany()
        {
            RegisterCompanyInput input = new RegisterCompanyInput()
            {
                Title = _viewModel.Title,
                Email = _viewModel.Email,
                Password = _viewModel.Password,
                PasswordConfirmtion = _viewModel.PasswordConfirmtion,
            };

            IValidator validator = new RegisterCompanyInputValidator(input, _companyService);

            bool validationResult = await validator.Validate();

            if (!validationResult)
            {
                var firstError = validator.Fails.First();
                await Application.Current.MainPage.DisplayAlert(firstError.Key, firstError.Value, "Ок");
                return;
            }

            string json = JsonConvert.SerializeObject(input);

            await _dialogService.ShowDialogAsync("Аккаунт комании создан", "Заполните информацию о вашей компании", "Заполнить информацию",
                async () => await Shell.Current.GoToAsync($"{nameof(CompanyCardPage)}?Input={json}"));
        }

        private void OnCanExecuteChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.CanRegister)) CanExecuteChanged?.Invoke(this, e);
        }
    }
}
