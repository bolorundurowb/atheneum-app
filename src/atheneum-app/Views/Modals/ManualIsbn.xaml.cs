using System;
using AtheneumApp.Library.DataAccess.Implementations;
using AtheneumApp.Library.Extensions;
using AtheneumApp.Library.Models.View;
using AtheneumApp.Utils;
using Refit;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms.Xaml;

namespace AtheneumApp.Views.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManualIsbn : Popup
    {
        private readonly BookService _bookService;

        public ManualIsbn()
        {
            InitializeComponent();
            _bookService = BookService.Instance();
        }

        protected async void Add(object sender, EventArgs e)
        {
            const string genericErrorMessage =
                "Sorry, an error occurred when adding the book to your library. Try again later.";
            var isbn = txtIsbn.Text;

            if (string.IsNullOrWhiteSpace(isbn))
            {
                ToastService.Error("An isbn is required.");
                return;
            }

            stkBtns.IsVisible = false;
            prgAdding.IsVisible = true;

            try
            {
                await _bookService.AddByIsbn(isbn);

                // notify user
                ToastService.Success("Book successfully added to your library.");

                // send data back
                Dismiss("");
            }
            catch (ApiException ex) when (ex.IsValidationException())
            {
                var error = await ex.GetContentAsAsync<ValidationErrorViewModel>();
                ToastService.Error(error?.Message?.Length > 0 ? error.Message[0] : genericErrorMessage);
            }
            catch (ApiException ex)
            {
                var error = await ex.GetContentAsAsync<ErrorViewModel>();
                ToastService.Error(!string.IsNullOrWhiteSpace(error?.Message) ? error.Message : genericErrorMessage);
            }
            finally
            {
                stkBtns.IsVisible = true;
                prgAdding.IsVisible = false;
            }
        }

        protected void Cancel(object sender, EventArgs e)
        {
            Dismiss(null);
        }

        protected override object GetLightDismissResult() => null;
    }
}
