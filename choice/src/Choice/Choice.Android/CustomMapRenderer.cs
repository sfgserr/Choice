using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Choice.CustomControls;
using Choice.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace Choice.Droid
{   
    public class CustomMapRenderer : MapRenderer
    {
        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);
            map.UiSettings.ZoomControlsEnabled = false;
            map.UiSettings.MyLocationButtonEnabled = false;
            map.UiSettings.ZoomGesturesEnabled = false;
            map.UiSettings.ScrollGesturesEnabled = false;
        }
    }
}