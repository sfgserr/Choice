using Choice.Commands;
using Choice.Dialogs.CategoriesDialogs;
using Choice.Dialogs.LinkSocialMediaDialogs;
using Choice.Services.AuthenticationServices;
using Choice.Services.CategoryApiServices;
using Choice.Stores.IndexStores;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web;
using System.Windows.Input;
using Xamarin.Forms;

namespace Choice.ViewModels
{
    public class CompanyCardViewModel : ViewModelBase, IQueryAttributable
    {
        private readonly IIndexStore _indexStore;
        private readonly ICategoryApiService _categoryApiService;

        public CompanyCardViewModel(IIndexStore indexStore, ICategoryApiService categoryApiService)
        {
            _indexStore = indexStore;
            _indexStore.StateChanged += OnIndexChanged;

            _categoryApiService = categoryApiService;

            NavigateBackCommand = new RelayCommand(par => { if (_indexStore.State != 1) _indexStore.State--; });
        }

        public string PageThreeColor => Index == 3 ? "#2688EB" : "#DFDFDF";
        public string PageTwoColor => Index >= 2 ? "#2688EB" : "#DFDFDF";
        public string PageOneColor => Index >= 1 ? "#2688EB" : "#DFDFDF";
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

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            string json = HttpUtility.UrlDecode(query["Input"]);
            RegisterCompanyInput input = JsonConvert.DeserializeObject<RegisterCompanyInput>(json);

            CompanyContactDataViewModel = new CompanyContactDataViewModel(_indexStore, input);
            CompanySocialMediasViewModel = new CompanySocialMediasViewModel(input, _indexStore, new LinkSocialMediaDialogService());
            CompanyDescriptionViewModel = new CompanyDescriptionViewModel(input, _categoryApiService, new CategoriesDialogService());
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
