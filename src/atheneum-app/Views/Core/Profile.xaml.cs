using System;
using System.Threading.Tasks;
using atheneum_app.Library.DataAccess.Implementations;
using atheneum_app.Library.Extensions;
using atheneum_app.Library.Models.View;
using atheneum_app.Utils;
using atheneum_app.Views.Auth;
using Refit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace atheneum_app.Views.Core
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

            // pull local user info
            var (firstName, lastName) = TokenService.GetUserDetails();
            var emailAddress = TokenService.GetEmail();
            DisplayUser(string.Empty, firstName, lastName, emailAddress);

            // pull remote user data
            prgLoading.IsVisible = true;

            try
            {
                var user = await _userService.GetProfile();
                DisplayUser(user);
            }
            catch (ApiException ex) when (ex.IsValidationException())
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
                TokenService.SetUserDetails(user.FirstName, user.LastName);

                // notify user
                ToastService.Success("Profile updated successfully.");
            }
            catch (ApiException ex) when (ex.IsValidationException())
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
            catch (ApiException ex) when (ex.IsValidationException())
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

        protected async void LogOut(object sender, EventArgs e)
        {
            TokenService.ResetAuthToken();

            // route back to login
            var parent = Parent?.Parent?.Parent?.Parent?.Parent?.Parent as ContentPage;
            Navigation.InsertPageBefore(new Login(), parent);
            await Navigation.PopAsync();
        }

        private void DisplayUser(UserViewModel user)
        {
            DisplayUser(user.Id, user.FirstName, user.LastName, user.EmailAddress);
        }

        private void DisplayUser(string userId, string firstName, string lastName, string emailAddress)
        {
            lblId.Text = userId;
            txtFirstName.Text = firstName;
            txtLastName.Text = lastName;
            txtEmail.Text = emailAddress;
        }
    }
}
