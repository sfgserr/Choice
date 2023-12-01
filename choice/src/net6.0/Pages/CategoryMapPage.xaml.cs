
using Choice.ViewModels;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace Choice.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryMapPage : ContentPage
    {
        private readonly CategoryMapViewModel _viewModel;

        public CategoryMapPage(CategoryMapViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;

            BindingContext = _viewModel;

            SetupMap();
        }

        public void SetupMap()
        {
            Location position = new Location(39, 39);
            Distance distance = Distance.FromMeters(3000);

            UserMapViewModel viewModel = new UserMapViewModel(_viewModel.User);

            Pin pin = new Pin()
            {
                Location = position,
            };

            map.Pins.Add(pin);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(position, distance));
        }
    }
}