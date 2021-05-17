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
    public partial class Register : ContentPage
    {
        private readonly AuthService _authClient;

        public Register()
        {
            InitializeComponent();
            _authClient = new AuthService();
            AddGestureRecognizers();
        }

        protected async void AttemptRegister(object sender, EventArgs e)
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

            btnRegister.IsVisible = false;
            prgLoading.IsVisible = true;

            try
            {
                var response = await _authClient.Register(email, password);

                var tokenClient = new TokenService();
                tokenClient.SetAuth(response.FullName, email, response.AuthToken);

                Toasts.DisplaySuccess("Registered successfully.");

                // send to home page
                Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync();
            }
            catch (ApiException ex) when (ex.StatusCode is HttpStatusCode.BadRequest)
            {
                var error = await ex.GetContentAsAsync<ValidationErrorViewModel>();

                Toasts.DisplayError(error?.Message?.Length > 0
                    ? error.Message[0]
                    : "Sorry, an error occurred when registering you. Try again later.");
            }
            catch (ApiException ex)
            {
                var error = await ex.GetContentAsAsync<ErrorViewModel>();

                Toasts.DisplayError(!string.IsNullOrWhiteSpace(error?.Message)
                    ? error.Message
                    : "Sorry, an error occurred when registering you. Try again later.");
            }
            finally
            {
                btnRegister.IsVisible = true;
                prgLoading.IsVisible = false;
            }
        }

        private void AddGestureRecognizers()
        {
            var loginTapRecognizer = new TapGestureRecognizer();
            loginTapRecognizer.Tapped += async (sender, e) => { await Navigation.PopAsync(); };
            lblLogin.GestureRecognizers.Add(loginTapRecognizer);
        }
    }
}
