using Choice.Domain.Models;
using Choice.Pages;
using Choice.Services.CompanyApiService;
using Choice.ViewModels;
using Newtonsoft.Json;
using System.Windows.Input;

namespace Choice.Commands
{
    public class NavigateToCategoryMapPageCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly CategoryListViewModel _viewModel;
        private readonly ICompanyApiService _companyService;

        public NavigateToCategoryMapPageCommand(CategoryListViewModel viewModel, ICompanyApiService companyService)
        {
            _viewModel = viewModel;
            _companyService = companyService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            IList<Company> companies = await _companyService.GetAll();

            companies = companies.Where(c => c.Categories.Any(ct => ct.Id == _viewModel.Id)).ToList();

            string json = JsonConvert.SerializeObject(companies);

            await Shell.Current.GoToAsync($"{nameof(CategoryMapPage)}?Companies={json}");
        }
    }
}
