using Choice.Stores.Navigators;
using System;

namespace Choice.ViewModels.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<LoginViewModel> _login;

        public ViewModelFactory(CreateViewModel<LoginViewModel> login)
        {
            _login = login;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            return viewType switch
            {
                ViewType.Login => _login(),
                _ => throw new ArgumentException()
            };
        }
    }
}
