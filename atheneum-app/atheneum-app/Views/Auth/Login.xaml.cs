using System;
using System.Net;
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
            AddGestureRecognizers();
        }

        protected async void AttemptLogin(object sender, EventArgs e)
        {
            var email = txtEmail.Text;
            var password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(email))
            {
                Toasts.DisplayError("An email address is required.");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                Toasts.DisplayError("A password is required.");
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
            catch (ApiException ex) when (ex.StatusCode is HttpStatusCode.BadRequest)
            {
                var error = await ex.GetContentAsAsync<ValidationErrorViewModel>();

                Toasts.DisplayError(error?.Message?.Length > 0
                    ? error.Message[0]
                    : "Sorry, an error occurred when logging you in. Try again later.");
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

        private void AddGestureRecognizers()
        {
            var signUpTapRecognizer = new TapGestureRecognizer();
            signUpTapRecognizer.Tapped += async (sender, e) => { await Navigation.PushAsync(new Register()); };
            lblSignUp.GestureRecognizers.Add(signUpTapRecognizer);
        }
    }
}
