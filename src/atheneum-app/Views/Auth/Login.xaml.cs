using System;
using System.Net;
using atheneum_app.Library.DataAccess.Implementations;
using atheneum_app.Library.Models.View;
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
            _authClient = AuthService.Instance();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // if user has logged in before, help autofill
            var email = TokenService.GetEmail();

            if (!string.IsNullOrWhiteSpace(email))
            {
                txtEmail.Text = email;
            }
        }

        protected async void AttemptLogin(object sender, EventArgs e)
        {
            const string genericErrorMessage = "Sorry, an error occurred when logging you in. Try again later.";
            var email = txtEmail.Text;
            var password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(email))
            {
                ToastService.Error("An email address is required.");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                ToastService.Error("A password is required.");
                return;
            }

            btnLogin.IsVisible = false;
            prgLoading.IsVisible = true;

            try
            {
                var response = await _authClient.Login(email, password);
                TokenService.SetUserDetails(response.FirstName, response.LastName);
                TokenService.SetEmail(response.EmailAddress);
                TokenService.SetEmailVerified(response.IsEmailVerified);
                await TokenService.SetAuthToken(response.AuthToken);

                ToastService.Success("Logged in successfully.");

                if (response.IsEmailVerified)
                {
                    // send to home page
                    Navigation.InsertPageBefore(new Root(), this);
                    await Navigation.PopAsync();
                }
                else
                {
                    await Navigation.PushAsync(new VerifyEmail());
                }
            }
            catch (ApiException ex) when (ex.StatusCode is HttpStatusCode.BadRequest)
            {
                var error = await ex.GetContentAsAsync<ValidationErrorViewModel>();
                ToastService.Error(error?.Message?.Length > 0 ? error.Message[0] : genericErrorMessage);
            }
            catch (ApiException ex)
            {
                var error = await ex.GetContentAsAsync<ErrorViewModel>();
                ToastService.Error(!string.IsNullOrWhiteSpace(error?.Message) ? error.Message : genericErrorMessage);
            }
            finally
            {
                btnLogin.IsVisible = true;
                prgLoading.IsVisible = false;
            }
        }

        protected async void GoToRegister(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Register());
        }

        protected async void GoToForgotPassword(object sender, EventArgs e)
        {
            var email = txtEmail.Text;

            if (string.IsNullOrWhiteSpace(email))
            {
                await Navigation.PushAsync(new ForgotPassword());
            }
            else
            {
                await Navigation.PushAsync(new ForgotPassword(email));
            }
        }
    }
}
