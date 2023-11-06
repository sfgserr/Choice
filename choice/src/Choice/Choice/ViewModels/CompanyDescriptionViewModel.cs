using Choice.Domain.Models;
using Choice.Services.AuthenticationServices;
using Choice.Services.CategoryApiServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Choice.ViewModels
{
    public class CompanyDescriptionViewModel : ViewModelBase
    {
        private readonly ICategoryApiService _categoryApiService;

        public CompanyDescriptionViewModel(RegisterCompanyInput input, ICategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;

            Input = input;
        }

        public RegisterCompanyInput Input { get; set; }

        private List<Category> _categories;

        public List<Category> Categories
        {
            get => _categories;
            set => Set(ref _categories, value);
        }

        private async Task GetCategories()
        {
            IList<Category> categories = await _categoryApiService.GetAll();
            Categories = categories.ToList();
        }
    }
}
