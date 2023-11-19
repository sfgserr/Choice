using Choice.Domain.Models;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Xamarin.Forms;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Choice.ViewModels
{
    public class CategoryMapViewModel : ViewModelBase, IQueryAttributable
    {
        public CategoryMapViewModel()
        {
            Map = new Map(BasemapStyle.ArcGISNavigation);
        }

        private List<Company> _companies = new List<Company>();

        public List<Company> Companies
        {
            get => _companies;
            set => Set(ref _companies, value);
        }

        private Map _map;

        public Map Map
        {
            get => _map;
            set => Set(ref _map, value);
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            Companies = JsonConvert.DeserializeObject<List<Company>>(query["Companies"]);
        }
    }
}
