using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.AppCompat.App;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

namespace atheneum_app.Android
{
    [Activity(Theme = "@style/MainTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            // handle theming
            var isInDarkMode = AppCompatDelegate.DefaultNightMode == AppCompatDelegate.ModeNightYes;
            SetTheme(isInDarkMode
                ? Resource.Style.DarkTheme
                : Resource.Style.LightTheme);

            base.OnCreate(savedInstanceState);
            Forms.Init(this, savedInstanceState);
            UserDialogs.Init(this);
            LoadApplication(new App());

            // set the bottom bar colour
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                var colourCode = isInDarkMode ? 17 : 254;
                var colour = new Color(colourCode, colourCode, colourCode);
                Window?.SetNavigationBarColor(colour);
            }
        }
    }
}
