using Android.App;
using Android.Content;
using AndroidX.AppCompat.App;

namespace atheneum_app.Android
{
    [Activity(Label = "Atheneum", Theme = "@style/SplashDarkTheme", Icon = "@drawable/ic_app_icon",
        MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

        public override void OnBackPressed()
        {
        }
    }
}
