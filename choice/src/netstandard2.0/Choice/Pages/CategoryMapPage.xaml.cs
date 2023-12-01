using Choice.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
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

            UserMapViewModel viewModel = new UserMapViewModel(_viewModel.User);

            Pin pin = new Pin()
            {
                Position = position,
            };

            map.Pins.Add(pin);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(position, distance));
        }
    }
}