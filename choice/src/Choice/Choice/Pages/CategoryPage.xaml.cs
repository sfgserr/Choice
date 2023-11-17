using Choice.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Choice.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryPage : ContentPage
    {
        public CategoryPage()
        {
            InitializeComponent();
            BindingContext = ServicesContainer.GetService<CategoryViewModel>();
        }

        private void ViewCell_Tapped(object sender, System.EventArgs e)
        {
            ViewCell cell = (ViewCell)sender;
            cell.View.BackgroundColor = Color.White;
        }
    }
}