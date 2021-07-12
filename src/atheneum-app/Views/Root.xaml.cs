using System;
using atheneum_app.Services.Interfaces;
using atheneum_app.Utils;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace atheneum_app.Views
{
    public partial class Root : ContentPage
    {
        public Root()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                // load the library page data
                await pageLibrary.LoadData();

                // load the wishlist page data
                await pageWishList.LoadData();

                // load profile page data
                await pageProfile.LoadData();
            }
            catch (Exception ex)
            {
                ToastService.Error(ex.Message);
            }
        }

        protected async void Scan(object sender, TabTappedEventArgs e)
        {
            var scanner = DependencyService.Get<IBarcodeScanService>();
            var result = await scanner.ScanAsync();
        }
    }
}