using Choice.Commands;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Choice.ViewModels
{
    public class PhotoViewModel : ViewModelBase
    {
		private readonly Action _updateCanSaveCompanyData;

        public PhotoViewModel(Action updateCanSaveCompanyData)
        {
            _updateCanSaveCompanyData = updateCanSaveCompanyData;

            ClearSourceCommand = new ClearSourceCommand(this);
        }

        public ICommand ClearSourceCommand { get; }
		public int Padding => IsEmpty ? 35 : 0;
		public bool IsEmpty => Source.IsEmpty;
		public bool IsUpload => !Source.IsEmpty;

		private ImageSource _source = string.Empty;

        public ImageSource Source
		{
			get => _source;
			set 
			{
				Set(ref _source, value);

				OnPropertyChanged(nameof(IsUpload));
                OnPropertyChanged(nameof(IsEmpty));
				OnPropertyChanged(nameof(Padding));
				_updateCanSaveCompanyData();
            }
		}
	}
}
