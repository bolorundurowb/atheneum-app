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
            stkContent.IsVisible = false;
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
                stkContent.IsVisible = true;
                prgLoading.IsVisible = false;
            }
        }

        protected async void UpdateProfile(object sender, EventArgs e)
        {
            const string genericErrorMessage =
                "Sorry, an error occurred when updating your information. Try again later.";
            prgUpdateProfile.IsVisible = true;
            btnUpdateProfile.IsVisible = false;

            try
            {
                var firstName = txtFirstName.Text;
                var lastName = txtLastName.Text;
                await _userService.UpdateProfile(firstName, lastName);
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

        private void DisplayUser(UserViewModel user)
        {
            lblId.Text = user.Id;
            txtFirstName.Text = user.FirstName;
            txtLastName.Text = user.LastName;
            txtEmail.Text = user.EmailAddress;
        }
    }
}