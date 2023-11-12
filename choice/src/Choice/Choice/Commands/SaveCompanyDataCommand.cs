using Choice.Dialogs.AccountCreatedDialogs;
using Choice.Services.AuthenticationServices;
using Choice.Services.BlobServices;
using Choice.Stores.Loaders;
using Choice.ViewModels;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Choice.Commands
{
    public class SaveCompanyDataCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly ILoader _loader;
        private readonly IAuthenticationService _authenticationService;
        private readonly IAlertDialogService _alertDialogService;
        private readonly IBlobService _blobService;
        private readonly CompanyDescriptionViewModel _viewModel;

        public SaveCompanyDataCommand(CompanyDescriptionViewModel viewModel, IAuthenticationService authenticationService, IAlertDialogService alertDialogService, IBlobService blobService, ILoader loader)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnCanExecuteChanged;

            _authenticationService = authenticationService;
            _alertDialogService = alertDialogService;
            _blobService = blobService;
            _loader = loader;
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.CanSaveCompanyData;
        }

        public async void Execute(object parameter)
        {
            await _loader.Load(SaveCompanyData);
        }

        private async Task SaveCompanyData()
        {
            _viewModel.Input.PhotoUris = _viewModel.PhotoViewModels.Select(p => ((FileImageSource)p.Source).File).ToList();
            //_viewModel.Input.PhotoUris.ForEach(async p => await _blobService.UploadPhoto(p));
            await _authenticationService.RegisterCompany(_viewModel.Input);
            await _alertDialogService.ShowDialogAsync("Отлично!", "Теперь тысячи пользователей увидят вашу компанию, вы сможете отвечать на их запросы", "Понятно", async () => await Shell.Current.GoToAsync("../../"));
        }

        private void OnCanExecuteChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.CanSaveCompanyData)) CanExecuteChanged?.Invoke(sender, e);
        }
    }
}
