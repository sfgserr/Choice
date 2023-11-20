using Choice.ViewModels;
using System.Reflection;
using Twilio.Rest.Api.V2010.Account.Usage.Record;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace Choice.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryMapPage : ContentPage
    {
        private readonly CategoryMapViewModel _viewModel;

        public CategoryMapPage()
        {
            InitializeComponent();

            _viewModel = ServicesContainer.GetService<CategoryMapViewModel>();

            BindingContext = _viewModel;

            SetupMap();
        }

        public void SetupMap()
        {
            Position position = new Position(39, 39);
            Distance distance = Distance.FromMeters(3000);

            Pin pin = new Pin()
            {
                Icon = BitmapDescriptorFactory.From
            };

            map.MoveToRegion(MapSpan.FromCenterAndRadius(position, distance));
        }
    }
}