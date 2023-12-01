using Choice.Commands;
using Choice.Domain.Models;
using Choice.Services.CompanyApiService;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Choice.ViewModels
{
    public class CategoryListViewModel
    {
        public CategoryListViewModel(Category category, ICompanyApiService companyService)
        {
            Id = category.Id;
            Title = category.Title.Replace('_', ' ');
            ImageSource = ImageSource.FromFile($"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/{category.IconUri}.png");
            NavigateToCategoryMapPageCommand = new NavigateToCategoryMapPageCommand(this, companyService);
        }

        public int Id { get; set; } 
        public string Title { get; set; }
        public ImageSource ImageSource { get; set; }
        public ICommand NavigateToCategoryMapPageCommand { get; }
    }
}
