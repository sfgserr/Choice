using Choice.Domain.Models;
using Xamarin.Forms;

namespace Choice.ViewModels
{
    public class CategoryDialogViewModel : ViewModelBase
    {
        public CategoryDialogViewModel(Category category)
        {
            Category = category;
        }

        public Category Category { get; set; }
		public Color Color => IsChecked ? Color.FromHex("#FF3F8AE0") : Color.FromHex("#FFD5D5D7");

		private bool _isChecked = false;

        public bool IsChecked
		{
			get => _isChecked;
			set
			{
				Set(ref _isChecked, value);
				OnPropertyChanged(nameof(Color));
			}
		}
	}
}
