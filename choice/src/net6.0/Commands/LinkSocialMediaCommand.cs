using Choice.Dialogs.LinkSocialMediaDialogs;
using Choice.ViewModels;
using System.Reflection;
using System.Windows.Input;

namespace Choice.Commands
{
    class LinkSocialMediaCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly ILinkSocialMediaDialogService _dialogService;
        private readonly CompanySocialMediasViewModel _viewModel;

        public LinkSocialMediaCommand(ILinkSocialMediaDialogService dialogService, CompanySocialMediasViewModel viewModel)
        {
            _dialogService = dialogService;
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            string socialMediaName = parameter.ToString();

            Action<string> action = s => SaveButtonClicked(socialMediaName, s);

            await _dialogService.ShowDialogAsync(socialMediaName, action);
        }

        public void SaveButtonClicked(string socialMediaName, string uri)
        {
            Type viewModel = _viewModel.GetType();

            PropertyInfo property = viewModel.GetProperty(socialMediaName + "Uri");

            property.SetValue(_viewModel, uri, null);
        }
    }
}
