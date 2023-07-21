using System;
using System.Linq;
using AtheneumApp.Library;
using AtheneumApp.Library.DataAccess.Implementations;
using AtheneumApp.Library.Extensions;
using AtheneumApp.Library.Models.View;
using AtheneumApp.Utils;
using Refit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AtheneumApp.Views.Auth
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
            _authClient = AuthService.Instance();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            txtEmail.Text = _emailAddress;
        }

        protected async void Reset(object sender, EventArgs e)
        {
            const string errorMessage = "Sorry, an error occurred when requesting a reset code. Try again later.";
            var resetCode = txtResetCode.Text;
            var password = txtPassword.Text;
            var confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrWhiteSpace(resetCode))
            {
                ToastService.Error("A reset code is required.");
                return;
            }

            if (resetCode.Length != Constants.ResetCodeLength)
            {
                ToastService.Error($"Reset code must be {Constants.ResetCodeLength} digits long.");
                return;
            }

            if (!resetCode.All(char.IsDigit))
            {
                ToastService.Error("Reset code can only contain digits.");
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

            btnReset.IsVisible = false;
            prgLoading.IsVisible = true;

            try
            {
                var response = await _authClient.ResetPassword(_emailAddress, resetCode, password);
                ToastService.Success(response.Message);

                // go back to login
                Navigation.InsertPageBefore(new Login(), this);
                await Navigation.PopAsync();
            }
            catch (ApiException ex) when (ex.IsValidationException())
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
