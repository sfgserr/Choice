using Choice.Dialogs.CategoriesDialogs;
using Choice.ViewModels;
using System;
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

        public void Execute(object parameter)
        {
            
        }
    }
}
