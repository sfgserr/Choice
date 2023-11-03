﻿using Choice.Dialogs.LinkSocialMediaDialogs;
using Choice.Services.AuthenticationServices;
using Choice.Stores.IndexStores;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web;
using Xamarin.Forms;

namespace Choice.ViewModels
{
    public class CompanyCardViewModel : ViewModelBase, IQueryAttributable
    {
        private readonly IIndexStore _indexStore;

        public CompanyCardViewModel(IIndexStore indexStore)
        {
            _indexStore = indexStore;
            _indexStore.StateChanged += OnIndexChanged;
        }

        public string PageThreeColor => Index == 3 ? "#2688EB" : "#DFDFDF";
        public string PageTwoColor => Index >= 2 ? "#2688EB" : "#DFDFDF";
        public string PageOneColor => Index >= 1 ? "#2688EB" : "#DFDFDF";
        public bool IsPageOneVisible => Index == 1;
        public bool IsPageTwoVisible => Index == 2;
        public bool IsPageThreeVisible => Index == 3;

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

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            string json = HttpUtility.UrlDecode(query["Input"]);
            RegisterCompanyInput input = JsonConvert.DeserializeObject<RegisterCompanyInput>(json);

            CompanyContactDataViewModel = new CompanyContactDataViewModel(_indexStore, input);
            CompanySocialMediasViewModel = new CompanySocialMediasViewModel(input, _indexStore, new LinkSocialMediaDialogService());
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
