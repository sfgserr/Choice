using Choice.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Choice
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(RegisterCompanyPage), typeof(RegisterCompanyPage));
            Routing.RegisterRoute(nameof(RegisterClientPage), typeof(RegisterClientPage));
            Routing.RegisterRoute(nameof(CompanyCardPage), typeof(CompanyCardPage));
            Routing.RegisterRoute(nameof(CategoryMapPage), typeof(CategoryMapPage));
        }
    }
}