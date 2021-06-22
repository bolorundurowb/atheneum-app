using atheneum_app.Library.DataAccess.Implementations;
using atheneum_app.Views.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("proxima.otf", Alias = "Proxima")]
[assembly: ExportFont("fredericka.ttf", Alias = "Fredericka")]
[assembly: ExportFont("faregular.otf", Alias = "FAR")]
[assembly: ExportFont("fasolid.otf", Alias = "FAS")]
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
                mainPage = new NavigationPage(new MainPage());
            }
            else
            {
                mainPage = new NavigationPage(new Login());
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
