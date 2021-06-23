using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace atheneum_app.Views.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetPassword : ContentPage
    {
        private readonly string _emailAddress;
        
        public ResetPassword(string emailAddress)
        {
            InitializeComponent();
            _emailAddress = emailAddress;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            txtEmail.Text = _emailAddress;
        }

        protected async void Reset(object sender, EventArgs e)
        {
            
        }

        protected async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}