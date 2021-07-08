using System;
using System.Net;
using System.Threading.Tasks;
using atheneum_app.Library.DataAccess.Implementations;
using atheneum_app.Library.Models.View;
using atheneum_app.Utils;
using Refit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace atheneum_app.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentView
    {
        private readonly UserService _userService;

        public Profile()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        public async Task LoadData()
        {
            const string genericErrorMessage =
                "Sorry, an error occurred when retrieving your information. Try again later.";
            prgLoading.IsVisible = true;

            try
            {
                var user = await _userService.GetProfile();
                DisplayUser(user);
            }
            catch (ApiException ex) when (ex.StatusCode is HttpStatusCode.BadRequest)
            {
                var error = await ex.GetContentAsAsync<ValidationErrorViewModel>();
                ToastService.Error(error?.Message?.Length > 0 ? error.Message[0] : genericErrorMessage);
            }
            catch (ApiException ex)
            {
                var error = await ex.GetContentAsAsync<ErrorViewModel>();
                ToastService.Error(error?.Message?.Length > 0 ? error.Message : genericErrorMessage);
            }
            finally
            {
                prgLoading.IsVisible = false;
            }
        }

        protected async void UpdateProfile(object sender, EventArgs e)
        {
            const string genericErrorMessage =
                "Sorry, an error occurred when updating your information. Try again later.";
            prgUpdateProfile.IsVisible = true;
            btnUpdateProfile.IsVisible = false;

            var firstName = txtFirstName.Text;
            var lastName = txtLastName.Text;

            try
            {
                var user = await _userService.UpdateProfile(firstName, lastName);

                // update the details locally
                var tokenService = new TokenService();
                tokenService.SetUserDetails(user.FirstName, user.LastName);

                // notify user
                ToastService.Success("Profile updated successfully.");
            }
            catch (ApiException ex) when (ex.StatusCode is HttpStatusCode.BadRequest)
            {
                var error = await ex.GetContentAsAsync<ValidationErrorViewModel>();
                ToastService.Error(error?.Message?.Length > 0 ? error.Message[0] : genericErrorMessage);
            }
            catch (ApiException ex)
            {
                var error = await ex.GetContentAsAsync<ErrorViewModel>();
                ToastService.Error(error?.Message?.Length > 0 ? error.Message : genericErrorMessage);
            }
            finally
            {
                btnUpdateProfile.IsVisible = true;
                prgUpdateProfile.IsVisible = false;
            }
        }

        protected async void UpdatePassword(object sender, EventArgs e)
        {
            const string genericErrorMessage =
                "Sorry, an error occurred when updating your password. Try again later.";
            var currentPassword = txtCurrentPassword.Text;
            var newPassword = txtNewPassword.Text;
            var confirmNewPassword = txtConfirmNewPassword.Text;

            if (string.IsNullOrWhiteSpace(currentPassword))
            {
                ToastService.Error("A current password is required.");
                return;
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                ToastService.Error("A new password is required.");
                return;
            }

            if (string.IsNullOrWhiteSpace(confirmNewPassword))
            {
                ToastService.Error("A password confirmation is required.");
                return;
            }

            if (newPassword != confirmNewPassword)
            {
                ToastService.Error("The password and confirmation don't match.");
                return;
            }

            prgUpdatePassword.IsVisible = true;
            btnUpdatePassword.IsVisible = false;

            try
            {
                var response = await _userService.UpdatePassword(currentPassword, newPassword);
                ToastService.Success(response.Message);
            }
            catch (ApiException ex) when (ex.StatusCode is HttpStatusCode.BadRequest)
            {
                var error = await ex.GetContentAsAsync<ValidationErrorViewModel>();
                ToastService.Error(error?.Message?.Length > 0 ? error.Message[0] : genericErrorMessage);
            }
            catch (ApiException ex)
            {
                var error = await ex.GetContentAsAsync<ErrorViewModel>();
                ToastService.Error(error?.Message?.Length > 0 ? error.Message : genericErrorMessage);
            }
            finally
            {
                btnUpdatePassword.IsVisible = true;
                prgUpdatePassword.IsVisible = false;
            }
        }

        private void DisplayUser(UserViewModel user)
        {
            lblId.Text = user.Id;
            txtFirstName.Text = user.FirstName;
            txtLastName.Text = user.LastName;
            txtEmail.Text = user.EmailAddress;
        }
    }
}