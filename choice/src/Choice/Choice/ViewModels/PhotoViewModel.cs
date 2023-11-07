using Xamarin.Forms;

namespace Choice.ViewModels
{
    public class PhotoViewModel : ViewModelBase
    {
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
            }
		}
	}
}
