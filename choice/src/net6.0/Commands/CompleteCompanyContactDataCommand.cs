using Choice.Stores.IndexStores;
using Choice.ViewModels;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Choice.Commands
{
    public class CompleteCompanyContactDataCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly CompanyContactDataViewModel _viewModel;
        private readonly IIndexStore _indexStore;

        public CompleteCompanyContactDataCommand(CompanyContactDataViewModel viewModel, IIndexStore indexStore)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnCanExecuteChanged;

            _indexStore = indexStore;
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.CanCompleteContactData;
        }

        public void Execute(object parameter)
        {
            _indexStore.State++;
        }

        private void OnCanExecuteChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.CanCompleteContactData)) CanExecuteChanged?.Invoke(sender, e);
        }
    }
}
