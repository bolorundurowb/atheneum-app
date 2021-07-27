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
    public partial class Register : ContentPage
    {
        private readonly AuthService _authClient;

        public Register()
        {
            InitializeComponent();
            _authClient = AuthService.Instance();
        }

        protected async void AttemptRegister(object sender, EventArgs e)
        {
            const string genericErrorMessage = "Sorry, an error occurred when registering you. Try again later.";
            var fullName = txtFullName.Text;
            var email = txtEmail.Text;
            var password = txtPassword.Text;
            var confirmPassword = txtConfirmPassword.Text;

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

            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                ToastService.Error("A password confirmation is required.");
                return;
            }

            if (password != confirmPassword)
            {
                ToastService.Error("The password and confirmation don't match.");
                return;
            }

            btnRegister.IsVisible = false;
            prgLoading.IsVisible = true;

            try
            {
                var response = await _authClient.Register(fullName, email, password);

                TokenService.SetUserDetails(response.FirstName, response.LastName);
                TokenService.SetEmail(email);
                TokenService.SetEmailVerified(response.IsEmailVerified);
                await TokenService.SetAuthToken(response.AuthToken);

                ToastService.Success("Account created successfully.");

                // send to verification page
                await Navigation.PushAsync(new VerifyEmail());
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
                btnRegister.IsVisible = true;
                prgLoading.IsVisible = false;
            }
        }

        protected async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
