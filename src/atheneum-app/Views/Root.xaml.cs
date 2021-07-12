using System;
using System.Net;
using atheneum_app.Library.DataAccess.Implementations;
using atheneum_app.Library.Models.View;
using atheneum_app.Services.Interfaces;
using atheneum_app.Utils;
using Refit;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace atheneum_app.Views
{
    public partial class Root : ContentPage
    {
        private readonly BookService _bookService;

        public Root()
        {
            InitializeComponent();
            _bookService = new BookService();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                // load the library page data
                await pageLibrary.LoadData();

                // load the wishlist page data
                await pageWishList.LoadData();

                // load profile page data
                await pageProfile.LoadData();
            }
            catch (Exception ex)
            {
                ToastService.Error(ex.Message);
            }
        }

        protected async void Scan(object sender, TabTappedEventArgs e)
        {
            const string genericErrorMessage =
                "Sorry, an error occurred when adding the book to your library. Try again later.";
            var scanner = DependencyService.Get<IBarcodeScanService>();
            var result = await scanner.ScanAsync();

            if (string.IsNullOrEmpty(result))
            {
                ToastService.Warn("Scan did not return a result.");
                return;
            }
            
            try
            {
                await _bookService.AddByIsbn(result);
                ToastService.Success("Book successfully added to your library");
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
        }
    }
}