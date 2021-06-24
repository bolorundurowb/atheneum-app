﻿using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

namespace atheneum_app.Android
{
    // [Activity(Theme = "@style/MainTheme",
    //     ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode)]
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
            Forms.Init(this, savedInstanceState);
            UserDialogs.Init(this);
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
        }
    }
}