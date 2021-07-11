using System;
using atheneum_app.Utils;
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
    }
}