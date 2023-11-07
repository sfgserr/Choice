using Choice.Commands;
using Choice.Dialogs.CategoriesDialogs;
using Choice.Domain.Models;
using Choice.Services.AuthenticationServices;
using Choice.Services.CategoryApiServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Choice.ViewModels
{
    public class CompanyDescriptionViewModel : ViewModelBase
    {
        private readonly ICategoryApiService _categoryApiService;

        public CompanyDescriptionViewModel(RegisterCompanyInput input, ICategoryApiService categoryApiService, ICategoriesDialogService dialogService)
        {
            _categoryApiService = categoryApiService;

            Input = input;
            SetUpPhotos();

            SelectCategoryCommand = new SelectCategoryCommand(this, dialogService);
            DisplayUploadPhotoActionSheetCommand = new DisplayUploadPhotoActionSheetCommand(this);
        }

        public ICommand SelectCategoryCommand { get; }
        public ICommand DisplayUploadPhotoActionSheetCommand { get; }
        public List<CategoryViewModel> CategoryViewModels { get; set; }
        public List<PhotoViewModel> PhotoViewModels { get; set; }
        public RegisterCompanyInput Input { get; set; }
        public string SelectedCommandsTitles => string.Join(", ", Input.Categories.Select(c => c.Title));

        private List<Category> _categories;

        public List<Category> Categories
        {
            get => _categories;
            set
            {
                Set(ref _categories, value);
                CategoryViewModels = _categories.Select(c => new CategoryViewModel(c)).ToList();
            }
        }

        public async Task GetCategories()
        {
            IList<Category> categories = await _categoryApiService.GetAll();
            Categories = categories.ToList();
        }

        public void UpdateTitles()
        {
            OnPropertyChanged(nameof(SelectedCommandsTitles));
        }

        private void SetUpPhotos()
        {
            PhotoViewModels = new List<PhotoViewModel>();

            for (int i = 0; i < 6; i++)
                PhotoViewModels.Add(new PhotoViewModel());
        }
    }
}
