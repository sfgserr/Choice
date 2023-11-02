using Choice.Extensions;
using Choice.Services.AuthenticationServices;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xamarin.Forms;

namespace Choice.ViewModels
{
    public class CompanyCardViewModel : ViewModelBase, IQueryAttributable
    {
		private RegisterCompanyInput _registerCompanyInput;

        public string Title
        {
            get => _registerCompanyInput?.Title;
            set
            {
                _registerCompanyInput.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Email
        {
            get => _registerCompanyInput?.Email;
            set
            {
                _registerCompanyInput.Email = value;
                OnPropertyChanged(nameof(Email));
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
            }
        }

        public string SiteUri
        {
            get => _registerCompanyInput?.SiteUri;
            set
            {
                _registerCompanyInput.SiteUri = value;
                OnPropertyChanged(nameof(SiteUri));
            }
        }

        public string Address
        {
            get => _registerCompanyInput?.Address;
            set
            {
                _registerCompanyInput.Address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            string json = HttpUtility.UrlDecode(query["Input"]);
            _registerCompanyInput = JsonConvert.DeserializeObject<RegisterCompanyInput>(json);

            UpdateProperties();
        }

        private void UpdateProperties()
        {
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(PhoneNumber));
            OnPropertyChanged(nameof(Address));
            OnPropertyChanged(nameof(SiteUri));
        }
    }
}
