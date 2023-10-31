using Choice.Services.AuthenticationServices;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web;
using Xamarin.Forms;

namespace Choice.ViewModels
{
    public class CompanyCardViewModel : ViewModelBase, IQueryAttributable
    {
		private RegisterCompanyInput _registerCompanyInput;

		public RegisterCompanyInput RegisterCompanyInput
        {
			get => _registerCompanyInput;
			set => Set(ref _registerCompanyInput, value);
		}

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            string json = HttpUtility.UrlDecode(query["Input"]);
            RegisterCompanyInput = JsonConvert.DeserializeObject<RegisterCompanyInput>(json);
        }
    }
}
