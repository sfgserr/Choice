using ClientApp.ViewModels;
using System;

namespace ClientApp.Stores.Navigator
{
    public sealed class Navigator : INavigator
    {
        public event Action? StateChanged;

        private ViewModelBase _viewModel;

        public ViewModelBase State => _viewModel;

        public void SetState(ViewModelBase state)
        {
            _viewModel = state;
            StateChanged?.Invoke();
        }
    }
}
