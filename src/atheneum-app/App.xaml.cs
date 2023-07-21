using AtheneumApp.Library.DataAccess.Implementations;
using AtheneumApp.Views;
using AtheneumApp.Views.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("fasolid.otf", Alias = "FAS")]
[assembly: ExportFont("faregular.otf", Alias = "FAR")]
[assembly: ExportFont("proxima.otf", Alias = "Proxima")]
[assembly: ExportFont("fredericka.ttf", Alias = "Fredericka")]
[assembly: ExportFont("proximabold.otf", Alias = "ProximaBold")]
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AtheneumApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // determine page to navigate to
            var isLoggedIn = TokenService.IsLoggedIn().Result;

            ContentPage mainPage;

            if (isLoggedIn)
            {
                if (TokenService.GetIsEmailVerified())
                {
                    mainPage = new Root();
                }
                else
                {
                    mainPage = new VerifyEmail();
                }
            }
            else
            {
                mainPage = new Login();
            }

            MainPage = new NavigationPage(mainPage);
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
