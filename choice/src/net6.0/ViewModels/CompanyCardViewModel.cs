using Choice.Commands;
using Choice.Dialogs.CategoriesDialogs;
using Choice.Dialogs.LinkSocialMediaDialogs;
using Choice.Services.AuthenticationServices;
using Choice.Services.CategoryApiServices;
using Choice.Services.FileServices;
using Choice.Stores.IndexStores;
using Choice.Stores.Loaders;
using System.Windows.Input;

namespace Choice.ViewModels
{
    public class CompanyCardViewModel : ViewModelBase, IQueryAttributable
    {
        private readonly IIndexStore _indexStore;
        private readonly ICategoryApiService _categoryApiService;
        private readonly ILoader _loader;
        private readonly IAuthenticationService _authenticationService;
        private readonly IFileService _fileService;

        public CompanyCardViewModel(IIndexStore indexStore, ICategoryApiService categoryApiService, ILoader loader, IAuthenticationService service, IFileService fileService)
        {
            _indexStore = indexStore;
            _indexStore.StateChanged += OnIndexChanged;

            _loader = loader;
            _authenticationService = service;
            _categoryApiService = categoryApiService;
            _fileService = fileService;

            NavigateBackCommand = new RelayCommand(par => { if (_indexStore.State != 1) _indexStore.State--; });
        }

        public Color PageThreeColor => Index == 3 ? Color.FromHex("#2688EB") : Color.FromHex("#DFDFDF");
        public Color PageTwoColor => Index >= 2 ? Color.FromHex("#2688EB") : Color.FromHex("#DFDFDF");
        public Color PageOneColor => Index >= 1 ? Color.FromHex("#2688EB") : Color.FromHex("#DFDFDF");
        public bool IsPageOneVisible => Index == 1;
        public bool IsPageTwoVisible => Index == 2;
        public bool IsPageThreeVisible => Index == 3;

        public ICommand NavigateBackCommand { get; }
        public int Index => _indexStore.State;

        private CompanyContactDataViewModel _companyContactDataViewModel;

        public CompanyContactDataViewModel CompanyContactDataViewModel
        {
            get => _companyContactDataViewModel;
            set => Set(ref _companyContactDataViewModel, value);
        }

        private CompanySocialMediasViewModel _companySocialMediasViewModel;

        public CompanySocialMediasViewModel CompanySocialMediasViewModel
        {
            get => _companySocialMediasViewModel;
            set => Set(ref _companySocialMediasViewModel, value);
        }

        private CompanyDescriptionViewModel _companyDescriptionViewModel;

        public CompanyDescriptionViewModel CompanyDescriptionViewModel
        {
            get => _companyDescriptionViewModel;
            set => Set(ref _companyDescriptionViewModel, value);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            RegisterCompanyInput input = (RegisterCompanyInput)query["Input"];

            CompanyContactDataViewModel = new CompanyContactDataViewModel(_indexStore, input);
            CompanySocialMediasViewModel = new CompanySocialMediasViewModel(input, _indexStore, new LinkSocialMediaDialogService());
            CompanyDescriptionViewModel = new CompanyDescriptionViewModel(input, _categoryApiService, new CategoriesDialogService(), _loader, _authenticationService, _fileService);
        }

        private void OnIndexChanged()
        {
            OnPropertyChanged(nameof(Index));
            OnPropertyChanged(nameof(PageOneColor));
            OnPropertyChanged(nameof(PageTwoColor));
            OnPropertyChanged(nameof(PageThreeColor));
            OnPropertyChanged(nameof(IsPageOneVisible));
            OnPropertyChanged(nameof(IsPageTwoVisible));
            OnPropertyChanged(nameof(IsPageThreeVisible));
        }
    }
}
