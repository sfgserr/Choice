using Choice.Commands;
using Choice.Dialogs.LinkSocialMediaDialogs;
using Choice.Services.AuthenticationServices;
using Choice.Stores.IndexStores;
using System.Windows.Input;

namespace Choice.ViewModels
{
    public class CompanySocialMediasViewModel : ViewModelBase
    {
        private RegisterCompanyInput _input;

        private readonly IIndexStore _indexStore;

        public CompanySocialMediasViewModel(RegisterCompanyInput input, IIndexStore indexStore, ILinkSocialMediaDialogService dialogService)
        {
            _input = input;
            _indexStore = indexStore;

            LinkSocialMediaCommand = new LinkSocialMediaCommand(dialogService, this);
        }

        public ICommand LinkSocialMediaCommand { get; }

        private string _instagramUri = string.Empty;

        public string InstagramUri
        {
            get => _instagramUri;
            set => Set(ref _instagramUri, value);
        }

        private string _facebookUri = string.Empty;

        public string FacebookUri
        {
            get => _facebookUri;
            set => Set(ref _facebookUri, value);
        }

        private string _vkUri;

        public string VkUri
        {
            get => _vkUri;
            set => Set(ref _vkUri, value);
        }

        private string _telegramUri = string.Empty; 

        public string TelegramUri
        {
            get => _instagramUri;
            set => Set(ref _telegramUri, value);
        }
    }
}
