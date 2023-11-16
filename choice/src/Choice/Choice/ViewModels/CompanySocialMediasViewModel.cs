using Choice.Commands;
using Choice.Dialogs.LinkSocialMediaDialogs;
using Choice.Services.AuthenticationServices;
using Choice.Stores.IndexStores;
using System.Windows.Input;

namespace Choice.ViewModels
{
    public class CompanySocialMediasViewModel : ViewModelBase
    {
        public CompanySocialMediasViewModel(RegisterCompanyInput input, IIndexStore indexStore, ILinkSocialMediaDialogService dialogService)
        {
            Input = input;

            LinkSocialMediaCommand = new LinkSocialMediaCommand(dialogService, this);
            CompleteSocialMediasCommand = new CompleteCompanySocialMediaCommand(this, indexStore);
        }

        public RegisterCompanyInput Input { get; set; }
        public ICommand CompleteSocialMediasCommand { get; }
        public ICommand LinkSocialMediaCommand { get; }
        public bool CanCompleteSocialMedias => IsFacebookLinked || IsInstagramLinked || IsTelegramLinked || IsVkLinked;
        public bool IsInstagramLinked => !string.IsNullOrEmpty(InstagramUri);
        public bool IsFacebookLinked => !string.IsNullOrEmpty(FacebookUri);
        public bool IsVkLinked => !string.IsNullOrEmpty(VkUri);
        public bool IsTelegramLinked => !string.IsNullOrEmpty(TelegramUri);

        private string _instagramUri = string.Empty;

        public string InstagramUri
        {
            get => _instagramUri;
            set
            {
                Set(ref _instagramUri, value);
                OnPropertyChanged(nameof(IsInstagramLinked));
                OnPropertyChanged(nameof(CanCompleteSocialMedias));
            }
        }

        private string _facebookUri = string.Empty;

        public string FacebookUri
        {
            get => _facebookUri;
            set
            {
                Set(ref _facebookUri, value);
                OnPropertyChanged(nameof(IsFacebookLinked));
                OnPropertyChanged(nameof(CanCompleteSocialMedias));
            }
        }

        private string _vkUri = string.Empty;

        public string VkUri
        {
            get => _vkUri;
            set
            {
                Set(ref _vkUri, value);
                OnPropertyChanged(nameof(IsVkLinked));
                OnPropertyChanged(nameof(CanCompleteSocialMedias));
            }
        }

        private string _telegramUri = string.Empty; 

        public string TelegramUri
        {
            get => _telegramUri;
            set
            {
                Set(ref _telegramUri, value);
                OnPropertyChanged(nameof(IsTelegramLinked));
                OnPropertyChanged(nameof(CanCompleteSocialMedias));
            }
        }
    }
}
