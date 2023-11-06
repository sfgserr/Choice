using Choice.Domain.Models;
using Choice.Services.ApiServices;
using Choice.Services.AuthenticationServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Choice.ViewModels
{
    public class CompanyDescriptionViewModel : ViewModelBase
    {
        private readonly IApiService<Category> _categoryService;

        public CompanyDescriptionViewModel(RegisterCompanyInput input, IApiService<Category> categoryService)
        {
            Input = input;
            _categoryService = categoryService;
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
            Categories = await _categoryService.GetAll();
        }
    }
}
