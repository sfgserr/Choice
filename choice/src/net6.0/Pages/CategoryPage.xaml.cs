using Choice.ViewModels;

namespace Choice.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryPage : ContentPage
    {
        public CategoryPage(CategoryViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private void ViewCell_Tapped(object sender, System.EventArgs e)
        {
            ViewCell cell = (ViewCell)sender;
            cell.View.BackgroundColor = Color.FromHex("#fff");
        }
    }
}