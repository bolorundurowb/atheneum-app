using System;
using System.Linq;
using System.Net;
using atheneum_app.Library;
using atheneum_app.Library.DataAccess.Implementations;
using atheneum_app.Library.Models.View;
using atheneum_app.Utils;
using Refit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace atheneum_app.Views.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerifyEmail : ContentPage
    {
        private readonly AuthService _authClient;

        public VerifyEmail()
        {
            InitializeComponent();
            _authClient = AuthService.Instance();
        }

        protected async void Verify(object sender, EventArgs e)
        {
            const string errorMessage = "Sorry, an error occurred when verifying your email address. Try again later.";
            var verificationCode = txtVerificationCode.Text;

            if (string.IsNullOrWhiteSpace(verificationCode))
            {
                ToastService.Error("A verification code is required.");
                return;
            }

            if (verificationCode.Length != Constants.VerificationCodeLength)
            {
                ToastService.Error($"Verification code must be {Constants.VerificationCodeLength} digits long.");
                return;
            }

            if (!verificationCode.All(char.IsDigit))
            {
                ToastService.Error("Verification code can only contain digits.");
                return;
            }

            btnVerify.IsVisible = false;
            prgLoading.IsVisible = true;

            try
            {
                var response = await _authClient.VerifyEmail(verificationCode);
                
                TokenService.SetEmailVerified(true);

                ToastService.Success(response.Message);

                // go back to login
                Application.Current.MainPage = new NavigationPage(new Root());
                await Navigation.PopAsync();
            }
            catch (ApiException ex) when (ex.StatusCode is HttpStatusCode.BadRequest)
            {
                var error = await ex.GetContentAsAsync<ValidationErrorViewModel>();
                ToastService.Error(error?.Message?.Length > 0 ? error.Message[0] : errorMessage);
            }
            catch (ApiException ex)
            {
                var error = await ex.GetContentAsAsync<ErrorViewModel>();
                ToastService.Error(!string.IsNullOrWhiteSpace(error?.Message) ? error.Message : errorMessage);
            }
            finally
            {
                btnVerify.IsVisible = true;
                prgLoading.IsVisible = false;
            }
        }

        protected async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
