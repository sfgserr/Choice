using Choice.Domain.Models;
using Choice.Stores.Authenticators;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Choice.ViewModels
{
    public class CategoryMapViewModel : ViewModelBase, IQueryAttributable
    {
        private readonly IAuthenticator _authenticator;

        public CategoryMapViewModel(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
            _authenticator.StateChanged += OnStateChanged;
        }

        public User User => _authenticator.State;

        private List<Company> _companies = new List<Company>();

        public List<Company> Companies
        {
            get => _companies;
            set => Set(ref _companies, value);
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            Companies = JsonConvert.DeserializeObject<List<Company>>(query["Companies"]);
        }

        private void OnStateChanged()
        {
            OnPropertyChanged(nameof(User));
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Companies = (List<Company>)query["Companies"];
        }
    }
}
