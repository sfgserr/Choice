using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using Choice.Domain.Models;
using System.Linq;

namespace Choice.Dialogs.CategoriesDialogs
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategoriesDialog
	{
		private readonly List<Category> _categories;
		private readonly Func<bool, Task> _clicked;
		private readonly Action<Category> _select;

		public CategoriesDialog(Func<bool, Task> clicked, Action<Category> select, List<Category> categories)
		{
			InitializeComponent();

			_clicked = clicked;
			_categories = categories;
			_select = select;
		}

		private void Close(object sender, ClickedEventArgs e)
		{
			_clicked(true);
		}

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
			ImageButton button = (ImageButton)sender;
			string categoryTitle = button.CommandParameter.ToString();
			Category category = _categories.FirstOrDefault(c => c.Title == categoryTitle);
			_select(category);
        }
    }
}