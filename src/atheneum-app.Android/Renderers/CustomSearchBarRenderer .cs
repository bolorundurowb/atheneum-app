using Android.Content;
using Android.Widget;
using atheneum_app.Android.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(SearchBar), typeof(CustomSearchBarRenderer))]
namespace atheneum_app.Android.Renderers
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        private readonly Context _context;

        public CustomSearchBarRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                return;
            }

            var plateId = Resources?.GetIdentifier("android:id/search_plate", null, null);
            var plate = Control.FindViewById(plateId ?? 0);
            plate?.SetBackgroundColor(Color.Transparent);
        }
    }
}