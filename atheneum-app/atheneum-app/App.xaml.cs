﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("NY.otf", Alias = "NY")]
[assembly: ExportFont("SFPro.ttf", Alias = "SF")]
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace atheneum_app
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
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
