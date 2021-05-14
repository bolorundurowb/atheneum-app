using atheneum_app.DataAccess.Implementations;
using atheneum_app.Views.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("NY.otf", Alias = "NY")]
[assembly: ExportFont("SFProRegular.otf", Alias = "SF")]
[assembly: ExportFont("SFProThin.otf", Alias = "SFThin")]
[assembly: ExportFont("SFProBold.otf", Alias = "SFBold")]
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace atheneum_app
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // determine page to navigate to
            var tokenClient = new TokenService();
            var isLoggedIn = tokenClient.IsLoggedIn();

            NavigationPage mainPage;
            if (isLoggedIn)
            {
                mainPage = new NavigationPage(new MainPage())
                {
                    BarBackgroundColor = Color.FromHex("#000000")
                };
            }
            else
            {
                mainPage = new NavigationPage(new Login())
                {
                    BarBackgroundColor = Color.FromHex("#000000")
                };
            }

            MainPage = mainPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
