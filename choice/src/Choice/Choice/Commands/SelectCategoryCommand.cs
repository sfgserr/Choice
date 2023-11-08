using Choice.Dialogs.CategoriesDialogs;
using Choice.Domain.Models;
using Choice.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Choice.Commands
{
    public class SelectCategoryCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly CompanyDescriptionViewModel _viewModel;
        private readonly ICategoriesDialogService _dialogService;

        public SelectCategoryCommand(CompanyDescriptionViewModel viewModel, ICategoriesDialogService dialogService)
        {
            _viewModel = viewModel;
            _dialogService = dialogService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if (_viewModel.Categories is null)
                await _viewModel.GetCategories();
            
            await _dialogService.ShowDialog(AddCategory, _viewModel.CategoryViewModels, _viewModel.Input.Categories.Count);
        }

        private int AddCategory(CategoryViewModel category)
        {
            Category categoryToRemove = _viewModel.Input.Categories.FirstOrDefault(c => c.Title == category.Category.Title);

            if (categoryToRemove != null)
            {
                _viewModel.Input.Categories.Remove(categoryToRemove);
                _viewModel.UpdateTitles();
                category.IsChecked = false;
                _viewModel.UpdateCanSaveCompanyData();

                return _viewModel.Input.Categories.Count;
            }

            _viewModel.Input.Categories.Add(category.Category);
            _viewModel.UpdateTitles();
            category.IsChecked = true;
            _viewModel.UpdateCanSaveCompanyData();

            return _viewModel.Input.Categories.Count;
        }
    }
}
