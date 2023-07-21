using Android.Content;
using Android.Graphics;
using AndroidX.AppCompat.Widget;
using AtheneumApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(AndroidNavigationRenderer))]
namespace AtheneumApp.Droid.Renderers
{
    public class AndroidNavigationRenderer : NavigationPageRenderer
    {
        private Toolbar _toolbar;

        public AndroidNavigationRenderer(Context context) : base(context) { }
        
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            if (_toolbar == null)
            {
                return;
            }
            
            _toolbar.ChildViewAdded += Toolbar_ChildViewAdded;
        }

        private void Toolbar_ChildViewAdded(object sender, ChildViewAddedEventArgs e)
        {
            if (e.Child?.GetType() != typeof(AppCompatTextView))
            {
                return;
            }
            
            var textView = (AppCompatTextView) e.Child;
            textView.Typeface = Typeface.CreateFromAsset(Context?.Assets, "proxima.otf");
            _toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
        }
    }
}
