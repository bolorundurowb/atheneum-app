using System;
using atheneum_app.Library.DataAccess.Implementations;
using atheneum_app.Library.Extensions;
using atheneum_app.Library.Models.View;
using atheneum_app.Utils;
using Refit;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms.Xaml;

namespace atheneum_app.Views.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManualBookEntry : Popup
    {
        private readonly BookService _bookService;
        
        public ManualBookEntry()
        {
            InitializeComponent();
            _bookService = BookService.Instance();
        }

        protected async void Add(object sender, EventArgs e)
        {
            const string genericErrorMessage =
                "Sorry, an error occurred when adding the book to your library. Try again later.";
            var title = txtTitle.Text;
            var summary = txtSummary.Text;
            var isbn = txtIsbn.Text;
            var publishYear = txtPublishYear.Text;
            var authors = txtAuthors.Text;
            var publisher = txtPublisher.Text;
            var pageCount = txtPageCount.Text;

            if (string.IsNullOrWhiteSpace(title))
            {
                ToastService.Error("A title is required.");
                return;
            }

            stkBtns.IsVisible = false;
            prgAdding.IsVisible = true;

            try
            {
                 await _bookService.AddManual(title, summary, isbn, publishYear, authors, publisher, pageCount);
                
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

        protected override object GetLightDismissResult()
        {
            return null;
        }
    }
}
