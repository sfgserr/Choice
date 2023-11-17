using Choice.Domain.Models;
using Choice.Services.CategoryApiServices;
using Choice.Services.FileServices;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Internals;

namespace Choice.ViewModels
{
    public class CategoryViewModel : ViewModelBase
    {
        private readonly ICategoryApiService _categoryService;
        private readonly IFileService _fileService;

        public CategoryViewModel(ICategoryApiService categoryService, IFileService fileService)
        {
            _categoryService = categoryService;
            _fileService = fileService;

            GetCategories();
        }

        private List<CategoryListViewModel> _categories = new List<CategoryListViewModel>();

        public List<CategoryListViewModel> Categories
        {
            get => _categories;
            set => Set(ref _categories, value);
        }

        public async void GetCategories()
        {
            IList<Category> categories = await _categoryService.GetAll();
            categories.ForEach(async c => await _fileService.DownloadPhoto($"{c.IconUri}.png"));

            Categories = categories.Select(c => new CategoryListViewModel(c)).ToList();
        }
    }
}
