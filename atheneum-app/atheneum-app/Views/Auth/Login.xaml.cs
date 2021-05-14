using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using atheneum_app.DataAccess.Implementations;
using atheneum_app.Models.View;
using atheneum_app.Utils;
using Refit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace atheneum_app.Views.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private readonly AuthService _authClient;

        public Login()
        {
            InitializeComponent();
            _authClient = new AuthService();
        }

        protected async void AttemptLogin(object sender, EventArgs e)
        {
            var email = txtEmail.Text;
            var password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(email))
            {
                await DisplayAlert("Error", "An email address is required.", "Ok");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "A password is required.", "Ok");
                return;
            }

            btnLogin.IsVisible = false;
            prgLoading.IsVisible = true;

            try
            {
                var response = await _authClient.Login(email, password);

                var tokenClient = new TokenService();
                tokenClient.SetAuth(response.FullName, email, response.AuthToken);
                
                Toasts.DisplaySuccess("Login successfully.");

                // send to home page
                Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync();
            }
            catch (ApiException ex)
            {
                var error = await ex.GetContentAsAsync<ErrorViewModel>();

                Toasts.DisplayError(!string.IsNullOrWhiteSpace(error?.Message)
                    ? error.Message
                    : "Sorry, an error occurred when logging you in. Try again later.");
            }
            finally
            {
                btnLogin.IsVisible = true;
                prgLoading.IsVisible = false;
            }
        }
    }
}
