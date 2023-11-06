using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using Choice.Domain.Models;
using System.Linq;
using Choice.ViewModels;

namespace Choice.Dialogs.CategoriesDialogs
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategoriesDialog
	{
		private readonly List<CategoryViewModel> _categories;
		private readonly Func<bool, Task> _clicked;
		private readonly Action<CategoryViewModel> _select;

		public CategoriesDialog(Func<bool, Task> clicked, Action<CategoryViewModel> select, List<CategoryViewModel> categories)
		{
			InitializeComponent();

			categories.ForEach(c => c.Category.Title = c.Category.Title.Replace('_', ' '));

			_clicked = clicked;
			_categories = categories;
			_select = select;

			Data.ItemsSource = categories;
		}

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
			ImageButton button = (ImageButton)sender;

			button.BackgroundColor = button.BackgroundColor.ToHex() == "#FFD5D5D7" ? Color.FromHex("#3F8AE0") :
                button.BackgroundColor.ToHex() == "#FF3F8AE0" ? Color.FromHex("#FFD5D5D7") : button.BackgroundColor;

			string categoryTitle = button.CommandParameter.ToString();
			CategoryViewModel category = _categories.FirstOrDefault(c => c.Category.Title == categoryTitle);
			_select(category);
        }

        private void Close_Clicked(object sender, EventArgs e)
        {
            _clicked(true);
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
			ViewCell cell = (ViewCell)sender;
			cell.View.BackgroundColor = Color.White;
        }
    }
}