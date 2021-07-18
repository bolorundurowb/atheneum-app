using System;
using System.Net;
using atheneum_app.Library.DataAccess.Implementations;
using atheneum_app.Library.Models.View;
using atheneum_app.Utils;
using atheneum_app.Views.Books;
using atheneum_app.Views.Modals;
using Refit;
using Xamarin.CommunityToolkit.Extensions;
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
            _bookService = BookService.Instance();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                // load the home page data
                await pageHome.LoadData();

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

        protected async void ShowOptions(object sender, TabTappedEventArgs e)
        {
            // var action = await DisplayActionSheet("Scan Options", "Cancel", null, "ISBN Barcode Scan", "Manual ISBN");
            // switch (action)
            // {
            //     case "ISBN Barcode Scan":
            //         var scanner = new Scanner();
            //         scanner.Disappearing += ScannerOnDisappearing;
            //         await Navigation.PushAsync(scanner);
            //         break;
            //     case "Manual ISBN":
            //         var result = await Navigation.ShowPopupAsync(new ManualIsbn());
            //
            //         if (result == null)
            //         {
            //             ToastService.Info("Modal dismissed.");
            //             return;
            //         }
            //         
            //         // refresh the library
            //         await pageLibrary.LoadData();
            //         break;
            // }

            await Navigation.ShowPopupAsync(new ActionSheet());
        }

        private async void ScannerOnDisappearing(object sender, EventArgs e)
        {
            const string genericErrorMessage =
                "Sorry, an error occurred when adding the book to your library. Try again later.";
            var scanner = sender as Scanner;
            var result = scanner?.ScanResult;

            if (scanner != null)
            {
                // try disconnecting the event handler
                scanner.Disappearing -= ScannerOnDisappearing;
            }

            if (string.IsNullOrEmpty(result))
            {
                ToastService.Warn("Scan did not return a result.");
                return;
            }

            try
            {
                // notify the user
                ToastService.Info($"Scanned ISBN: {result}. Adding to your library...");

                // call the service
                await _bookService.AddByIsbn(result);
                ToastService.Success("Book successfully added to your library");

                // refresh the library
                await pageLibrary.LoadData();
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