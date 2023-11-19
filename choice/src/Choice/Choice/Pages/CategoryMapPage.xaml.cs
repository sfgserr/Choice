using Choice.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Choice.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryMapPage : ContentPage
    {
        public CategoryMapPage()
        {
            InitializeComponent();
            BindingContext = ServicesContainer.GetService<CategoryMapViewModel>();

            SetupMap();
        }

        public void SetupMap()
        {
            Position position = new Position(39, 39);
            Distance distance = Distance.FromMeters(3000);

            map.MoveToRegion(MapSpan.FromCenterAndRadius(position, distance));
            map.HasScrollEnabled = false;
        }
    }
}