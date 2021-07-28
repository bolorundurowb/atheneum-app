using System;
using atheneum_app.Library.DataAccess.Implementations;
using atheneum_app.Library.Enums;
using atheneum_app.Library.Extensions;
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
            var selection = await Navigation.ShowPopupAsync(new ActionSheet()) as ActionType?;

            if (selection == null)
            {
                return;
            }

            switch (selection.Value)
            {
                case ActionType.Manual:
                    var manualResult = await Navigation.ShowPopupAsync(new ManualBookEntry());

                    if (manualResult == null)
                    {
                        return;
                    }

                    // refresh the library
                    await pageHome.LoadData();
                    await pageLibrary.LoadData();
                    break;
                case ActionType.ByIsbn:
                    var result = await Navigation.ShowPopupAsync(new ManualIsbn());

                    if (result == null)
                    {
                        return;
                    }

                    // refresh the library
                    await pageHome.LoadData();
                    await pageLibrary.LoadData();
                    break;
                case ActionType.ByScan:
                    var scanner = new Scanner();
                    scanner.Disappearing += ScannerOnDisappearing;
                    await Navigation.PushAsync(scanner);
                    break;
            }
        }

        private async void ScannerOnDisappearing(object sender, EventArgs e)
        {
            const string genericErrorMessage =
                "Sorry, an error occurred when adding the book to your library. Try again later.";
            var scanner = sender as Scanner;
            var result = scanner?.ScanResult;

            // try disconnecting the event handler
            if (scanner != null)
            {
                scanner.Disappearing -= ScannerOnDisappearing;
            }

            if (string.IsNullOrEmpty(result))
            {
                return;
            }

            try
            {
                // notify the user
                ToastService.Info($"Scanned ISBN: {result}. Adding to your library.");

                // call the service
                await _bookService.AddByIsbn(result);
                ToastService.Success("Book successfully added to your library");

                // refresh the library
                await pageHome.LoadData();
                await pageLibrary.LoadData();
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
        }
    }
}
