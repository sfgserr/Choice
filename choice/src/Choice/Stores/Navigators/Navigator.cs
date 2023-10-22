using Choice.ViewModels;
using System;

namespace Choice.Stores.Navigators
{
    public class Navigator : INavigator
    {
        public event Action StateChanged;

		private ViewModelBase _currentViewModel;

		public ViewModelBase State
		{
			get => _currentViewModel;
			set 
			{
				_currentViewModel = value;
				StateChanged?.Invoke();
			}
		}
	}
}
