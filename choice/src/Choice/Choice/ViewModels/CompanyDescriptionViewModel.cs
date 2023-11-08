using Choice.Commands;
using Choice.Dialogs.AccountCreatedDialogs;
using Choice.Dialogs.CategoriesDialogs;
using Choice.Domain.Models;
using Choice.Services.AuthenticationServices;
using Choice.Services.BlobServices;
using Choice.Services.CategoryApiServices;
using Choice.Stores.Loaders;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Choice.ViewModels
{
    public class CompanyDescriptionViewModel : ViewModelBase
    {
        private readonly ICategoryApiService _categoryApiService;
        private readonly ILoader _loader;

        public CompanyDescriptionViewModel(RegisterCompanyInput input, ICategoryApiService categoryApiService, ICategoriesDialogService dialogService, ILoader loader, IAuthenticationService service)
        {
            _loader = loader;
            _loader.StateChanged += OnIsLoadingChanged;

            _categoryApiService = categoryApiService;

            Input = input;
            SetUpPhotos();

            SelectCategoryCommand = new SelectCategoryCommand(this, dialogService);
            DisplayUploadPhotoActionSheetCommand = new DisplayUploadPhotoActionSheetCommand(this);
            SaveCompanyDataCommand = new SaveCompanyDataCommand(this, service, new AlertDialogService(), new BlobService(), _loader);
        }

        public ICommand SelectCategoryCommand { get; }
        public ICommand SaveCompanyDataCommand { get; }
        public ICommand DisplayUploadPhotoActionSheetCommand { get; }
        public bool IsLoading => _loader.State;
        public bool CanSaveCompanyData => Input.Categories.Count > 0 && PhotoViewModels.Any(p => !p.Source.IsEmpty); 
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

        public void UpdateCanSaveCompanyData()
        {
            OnPropertyChanged(nameof(CanSaveCompanyData));
        }

        private void SetUpPhotos()
        {
            PhotoViewModels = new List<PhotoViewModel>();

            for (int i = 0; i < 6; i++)
                PhotoViewModels.Add(new PhotoViewModel(UpdateCanSaveCompanyData));
        }

        private void OnIsLoadingChanged()
        {
            OnPropertyChanged(nameof(IsLoading));
        }
    }
}
