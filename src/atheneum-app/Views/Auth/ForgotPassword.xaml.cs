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
    public partial class ForgotPassword : ContentPage
    {
        private readonly AuthService _authClient;

        public ForgotPassword()
        {
            InitializeComponent();
            _authClient = new AuthService();
        }

        protected async void AttemptRequest(object sender, EventArgs e)
        {
            var email = txtEmail.Text;

            if (string.IsNullOrWhiteSpace(email))
            {
                ToastService.DisplayError("An email address is required.");
                return;
            }

            btnRequest.IsVisible = false;
            prgLoading.IsVisible = true;

            try
            {
                var response = await _authClient.ForgotPassword(email);
                ToastService.DisplaySuccess(response.Message);

                // go 
                await Navigation.PushAsync(new ResetPassword(email));
            }
            catch (ApiException ex) when (ex.StatusCode is HttpStatusCode.BadRequest)
            {
                var error = await ex.GetContentAsAsync<ValidationErrorViewModel>();

                ToastService.DisplayError(error?.Message?.Length > 0
                    ? error.Message[0]
                    : "Sorry, an error occurred when requesting a reset code. Try again later.");
            }
            catch (ApiException ex)
            {
                var error = await ex.GetContentAsAsync<ErrorViewModel>();

                ToastService.DisplayError(!string.IsNullOrWhiteSpace(error?.Message)
                    ? error.Message
                    : "Sorry, an error occurred when requesting a reset code. Try again later.");
            }
            finally
            {
                btnRequest.IsVisible = true;
                prgLoading.IsVisible = false;
            }
        }

        protected async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}