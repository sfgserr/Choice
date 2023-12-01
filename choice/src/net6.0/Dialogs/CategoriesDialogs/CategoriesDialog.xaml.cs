using Choice.ViewModels;

namespace Choice.Dialogs.CategoriesDialogs
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategoriesDialog
	{
		private readonly List<CategoryDialogViewModel> _categories;
		private readonly Func<bool, Task> _clicked;
		private readonly Func<CategoryDialogViewModel, int> _select;

		public CategoriesDialog(Func<bool, Task> clicked, Func<CategoryDialogViewModel, int> select, List<CategoryDialogViewModel> categories, int count)
		{
			InitializeComponent();

			categories.ForEach(c => c.Category.Title = c.Category.Title.Replace('_', ' '));

			_clicked = clicked;
			_categories = categories;
			_select = select;

			Data.ItemsSource = categories;
			selectBtn.Text = count == 0 ? "Выбор" : $"Выбор ({count})";
		}

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
			ImageButton button = (ImageButton)sender;

			var btn = button.BackgroundColor.ToHex();

			button.BackgroundColor = button.BackgroundColor.ToHex() == "#D5D5D7" ? Color.FromHex("#3F8AE0") :
                button.BackgroundColor.ToHex() == "#3F8AE0" ? Color.FromHex("#D5D5D7") : button.BackgroundColor;

			string categoryTitle = button.CommandParameter.ToString();
			CategoryDialogViewModel category = _categories.FirstOrDefault(c => c.Category.Title == categoryTitle);

			int count = _select(category);

            selectBtn.Text = count == 0 ? "Выбрать" : $"Выбрать ({count})";
        }

        private void Close_Clicked(object sender, EventArgs e)
        {
            _clicked(true);
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
			ViewCell cell = (ViewCell)sender;
			cell.View.BackgroundColor = Color.FromHex("#fff");
        }
    }
}