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
    public partial class ResetPassword : ContentPage
    {
        private readonly AuthService _authClient;
        private readonly string _emailAddress;
        
        public ResetPassword(string emailAddress)
        {
            InitializeComponent();
            _emailAddress = emailAddress;
            _authClient = new AuthService();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            txtEmail.Text = _emailAddress;
        }

        protected async void Reset(object sender, EventArgs e)
        {
            var resetCode = txtResetCode.Text;
            var password = txtPassword.Text;
            var confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrWhiteSpace(resetCode))
            {
                ToastService.DisplayError("A reset code is required.");
                return;
            }

            if (resetCode.Length != Constants.ResetCodeLength)
            {
                ToastService.DisplayError($"Reset code must be {Constants.ResetCodeLength} digits long.");
                return;
            }

            if (!resetCode.All(char.IsDigit))
            {
                ToastService.DisplayError("Reset code can only contain digits.");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                ToastService.DisplayError("A password is required.");
                return;
            }

            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                ToastService.DisplayError("A password confirmation is required.");
                return;
            }

            if (password != confirmPassword)
            {
                ToastService.DisplayError("The password and confirmation don't match.");
                return;
            }

            btnReset.IsVisible = false;
            prgLoading.IsVisible = true;

            try
            {
                var response = await _authClient.ResetPassword(_emailAddress, resetCode, password);
                ToastService.DisplaySuccess(response.Message);

                // go back to login
                Navigation.InsertPageBefore(new Login(), this);
                await Navigation.PopAsync();
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
                btnReset.IsVisible = true;
                prgLoading.IsVisible = false;
            }
        }

        protected async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}