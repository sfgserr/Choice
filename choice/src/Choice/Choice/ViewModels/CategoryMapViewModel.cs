using Choice.Domain.Models;
using System.Collections.Generic;

namespace Choice.ViewModels
{
    public class CategoryMapViewModel : ViewModelBase
    {
        private List<Company> _companies = new List<Company>();

        public List<Company> Companies
        {
            get => _companies;
            set => Set(ref _companies, value);
        }
    }
}
