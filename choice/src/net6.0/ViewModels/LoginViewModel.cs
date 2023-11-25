using Choice.Commands;
using Choice.Stores.Authenticators;
using Choice.Stores.IndexStores;
using Choice.Stores.Loaders;
using System.Windows.Input;

namespace Choice.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IIndexStore _indexStore;

        public LoginViewModel(IIndexStore indexStore, LoginByEmailViewModel loginByEmail, LoginByPhoneViewModel loginByPhone)
        {
            _indexStore = indexStore;
            _indexStore.StateChanged += OnIndexChanged;

            LoginByEmailViewModel = loginByEmail;
            LoginByPhoneViewModel = loginByPhone;

            DisplayActionSheetCommand = new DisplayAccountCreationActionSheet();
            TabViewCommand = new TabViewCommand(_indexStore);
        }

        public bool LoginByEmailIsVisible => Index == 1;
        public bool LoginByPhoneIsVisible => Index == 2;
        public Color PageTwoColor => Index == 2 ? Color.FromHex("#2688EB") : Color.FromHex("#fff");
        public Color PageOneColor => Index == 1 ? Color.FromHex("#2688EB") : Color.FromHex("#fff");
        public Color EmailButtonColor => Index == 1 ? Color.FromHex("#000") : Color.FromHex("#818C99");
        public Color PhoneButtonColor => Index == 2 ? Color.FromHex("#000") : Color.FromHex("#818C99");
        public int Index => _indexStore.State;
        public ICommand DisplayActionSheetCommand { get; }
        public ICommand TabViewCommand { get; }

        private LoginByEmailViewModel _loginByEmailViewModel;

        public LoginByEmailViewModel LoginByEmailViewModel
        {
            get => _loginByEmailViewModel;
            set => Set(ref _loginByEmailViewModel, value);
        }

        private LoginByPhoneViewModel _loginByPhoneViewModel;

        public LoginByPhoneViewModel LoginByPhoneViewModel
        {
            get => _loginByPhoneViewModel;
            set => Set(ref _loginByPhoneViewModel, value);
        }

        private void OnIndexChanged()
        {
            OnPropertyChanged(nameof(Index));
            OnPropertyChanged(nameof(LoginByEmailIsVisible));
            OnPropertyChanged(nameof(LoginByPhoneIsVisible));
            OnPropertyChanged(nameof(PageTwoColor));
            OnPropertyChanged(nameof(PageOneColor));
            OnPropertyChanged(nameof(EmailButtonColor));
            OnPropertyChanged(nameof(PhoneButtonColor));
        }
    }
}
