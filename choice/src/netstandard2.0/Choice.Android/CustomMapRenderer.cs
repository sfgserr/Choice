using Android.Content;
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
        public CustomMapRenderer(Context context) : base(context)
        {
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);
            map.UiSettings.ScrollGesturesEnabled = false;
            map.UiSettings.ZoomGesturesEnabled = false;
            map.UiSettings.ZoomControlsEnabled = false;
        }
    }
}