using Choice.Commands;
using Choice.Extensions;
using Choice.Services.AuthenticationServices;
using Choice.Stores.IndexStores;
using System.Linq;
using System.Windows.Input;

namespace Choice.ViewModels
{
    public class CompanyContactDataViewModel : ViewModelBase
    {
        private RegisterCompanyInput _registerCompanyInput;

        private readonly IIndexStore _indexStore;

        public CompanyContactDataViewModel(IIndexStore indexStore, RegisterCompanyInput input)
        {
            _indexStore = indexStore;
            _registerCompanyInput = input;

            CompleteCompanyContactDataCommand = new CompleteCompanyContactDataCommand(this, _indexStore);
        }

        public ICommand CompleteCompanyContactDataCommand { get; }
        public bool CanCompleteContactData => !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Email) &&
                                              PhoneNumber.Length == 15 && !string.IsNullOrEmpty(SiteUri) &&
                                              !string.IsNullOrEmpty(Address);

        public string Title
        {
            get => _registerCompanyInput?.Title;
            set
            {
                _registerCompanyInput.Title = value;
                OnPropertyChanged(nameof(Title));
                OnPropertyChanged(nameof(CanCompleteContactData));
            }
        }

        public string Email
        {
            get => _registerCompanyInput?.Email;
            set
            {
                _registerCompanyInput.Email = value;
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(CanCompleteContactData));
            }
        }

        public string PhoneNumber
        {
            get => _registerCompanyInput is null ? string.Empty : _registerCompanyInput.PhoneNumber.FormatPhoneNumber();
            set
            {
                if (_registerCompanyInput is null)
                    return;

                _registerCompanyInput.PhoneNumber = new string(value.Where(c => char.IsDigit(c)).ToArray());
                OnPropertyChanged(nameof(PhoneNumber));
                OnPropertyChanged(nameof(CanCompleteContactData));
            }
        }

        public string SiteUri
        {
            get => _registerCompanyInput?.SiteUri;
            set
            {
                _registerCompanyInput.SiteUri = value;
                OnPropertyChanged(nameof(SiteUri));
                OnPropertyChanged(nameof(CanCompleteContactData));
            }
        }

        public string Address
        {
            get => _registerCompanyInput?.Address;
            set
            {
                _registerCompanyInput.Address = value;
                OnPropertyChanged(nameof(Address));
                OnPropertyChanged(nameof(CanCompleteContactData));
            }
        }
    }
}
