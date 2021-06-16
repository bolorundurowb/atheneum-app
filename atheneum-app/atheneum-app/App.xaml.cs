﻿using atheneum_app.DataAccess.Implementations;
using atheneum_app.Views.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("proxima.otf", Alias = "Proxima")]
[assembly: ExportFont("fredericka.ttf", Alias = "Fredericka")]
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
                    BarBackgroundColor = Color.FromHex(Constants.DarkColour),
                    BarTextColor = Color.FromHex(Constants.PrimaryAccentColour)
                };
            }
            else
            {
                mainPage = new NavigationPage(new Login())
                {
                    BarBackgroundColor = Color.FromHex(Constants.DarkColour),
                    BarTextColor = Color.FromHex(Constants.PrimaryAccentColour)
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
