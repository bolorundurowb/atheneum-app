using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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
            
            // try to load user data on creation (no way to destroy it though
            LoadData().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private async Task LoadData()
        {
            const string genericErrorMessage = "Sorry, an error occurred when retrieving your information. try again later.";
            
            try
            {
                var user = await _userService.GetProfile();
                
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
    }
}