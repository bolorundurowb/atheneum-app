using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using FFImageLoading.Forms.Platform;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Color = Android.Graphics.Color;

namespace atheneum_app.Android
{
    [Activity(Label = "Atheneum", Icon = "@drawable/ic_app_icon", Theme = "@style/SplashTheme",
        MainLauncher = true, NoHistory = true, LaunchMode = LaunchMode.SingleTop,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            
            SetTheme(Resource.Style.MainTheme);
            base.OnCreate(savedInstanceState);
            
            CachedImageRenderer.Init(true);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            Forms.Init(this, savedInstanceState);
            CachedImageRenderer.InitImageViewHandler();
            Acr.UserDialogs.UserDialogs.Init(this);
            LoadApplication(new App());

            // set the bottom bar colour
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                var mode = Resources?.Configuration?.UiMode & UiMode.NightMask;
                var isInDarkMode = mode == UiMode.NightYes;
                var colourCode = isInDarkMode ? 17 : 254;
                var colour = new Color(colourCode, colourCode, colourCode);
                Window?.SetNavigationBarColor(colour);
            }

            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>()
                .UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}