using Choice.ViewModels;

namespace Choice.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompanyCardPage : ContentPage
    {
        public CompanyCardPage(CompanyCardViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}