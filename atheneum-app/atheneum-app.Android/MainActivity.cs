using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace atheneum_app.Android
{
    [Activity(Label = "Atheneum", Theme = "@style/MainTheme", MainLauncher = true, Icon = "@drawable/ic_app_icon",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Forms.Init(this, savedInstanceState);
            UserDialogs.Init(this);
            LoadApplication(new App());
        }
    }
}
