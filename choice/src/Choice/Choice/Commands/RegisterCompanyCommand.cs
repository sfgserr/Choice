using Choice.Pages;
using Choice.Services.AuthenticationServices;
using Choice.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Choice.Commands
{
    public class RegisterCompanyCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly RegisterCompanyViewModel _viewModel;

        public RegisterCompanyCommand(RegisterCompanyViewModel viewModel)
        {
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
                RegisterCompanyInput input = new RegisterCompanyInput()
                {
                    Title = _viewModel.Title,
                    Email = _viewModel.Email,
                    Password = _viewModel.Password,
                    PasswordConfirmtion = _viewModel.PasswordConfirmtion,
                };

                string json = JsonConvert.SerializeObject(input);

                await Shell.Current.GoToAsync($"{nameof(CompanyCardPage)}?Input={json}");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Внимание", ex.Message, "OK");
            }
        }

        private void OnCanExecuteChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.CanRegister)) CanExecuteChanged?.Invoke(this, e);
        }
    }
}
