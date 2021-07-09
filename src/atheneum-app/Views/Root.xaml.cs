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

            // load profile page data
            await pageProfile.LoadData();


            // load the wishlist page data
            await pageWishList.LoadData();
        }
    }
}